import traceback
import shelve
import logging
from logging import handlers
import json
from pprint import pformat
import datetime
import requests
from flask import Flask, request, render_template

import key

# disable flask and requests info logs
logging.getLogger('werkzeug').setLevel(logging.ERROR)
logging.getLogger("requests").setLevel(logging.ERROR)

app = Flask(__name__)

logger = logging.getLogger()
logger.setLevel(level=logging.INFO)
file_handler = handlers.RotatingFileHandler(
    'logs/server.log', maxBytes=10 * 1024 * 1024, backupCount=1, encoding='utf-8')
formatter = logging.Formatter('%(asctime)s %(levelname)s: %(message)s')
file_handler.setFormatter(formatter)
logger.addHandler(file_handler)

@app.route('/api/logerror', methods=['POST'])
def log_error():
    with open('logs/errors.txt', 'a+', encoding='utf-8') as f:
        f.write(request.data.decode('utf-8') + '\n\n')
    return "OK", 200


@app.route('/api/showerrors', methods=['GET'])
def show_errors():
    with open('logs/errors.txt', 'r') as f:
        return f.read(), 200


@app.route('/api/showdb', methods=['GET'])
def show_db():
    with shelve.open('database/clients') as db:
        try:
            return render_template('clients.html', database=dict(db), pprint=pformat), 200
        except:
            logger.error(traceback.print_exc()), 500


@app.route('/api/getlicense/<int:subscription_time>', methods=['GET'])
def get_license(subscription_time):
    try:
        licenses = key.generate(subscription_time)
        return ",".join(licenses), 200
    except Exception:
        error = traceback.print_exc()
        logger.error(error)
        return error, 500


@app.route('/api/getlicensestatus', methods=['POST'])
def get_license_status():
    keys = request.data.decode("utf-8").split(",")
    response = {}
    with shelve.open("database/clients") as db:
        for item in keys:
            try:
                response[item] = db[item]
            except KeyError:
                response[item] = None
                
    return json.dumps(response), 200


@app.route('/api/extendlicense', methods=['POST'])
def extend_license():
    data = {key: value for key, value in request.form.items()}
    try:
        key = data["key"]
        subscription_time = int(data["subscription_time"])
        with shelve.open("database/clients", writeback=True) as db:
            db[key]["subscription_time"] += subscription_time
        return "OK", 200
    except Exception:
        error = traceback.print_exc()
        logger.error(error)
        return error, 500


@app.route('/api/checklicense', methods=['POST'])
def check_license():
    success = False
    data = {key: value for key, value in request.form.items()}
    ip = request.environ.get('HTTP_X_REAL_IP', request.remote_addr)
    try:
        key = data['key']
    except KeyError:
        return success

    db = shelve.open("database/clients", writeback=True)
    try:
        if key in db:
            client = db[key]
            if client["subscription_time"] == 0:
                success = False
            else:
                hwid = data["hwid"]
                active_devices = client["devices"]
                if hwid not in active_devices:
                    city = get_city_from_ip(ip)
                    result = {
                        "connection_date": datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
                        "ip": (ip, city)
                    }
                    active_devices[hwid] = result
                    if client.get("initial_device", None) is None:
                        result["hwid"] = hwid
                        client["initial_device"] = result
                    logger.info("New device connected!: %s", active_devices[hwid])
                success = True
        else:
            logger.info('WRONG KEY: %s, %s\n', data, ip)
    finally:
        db.close()

    return json.dumps({'success_3248237582': success}), 200


def get_city_from_ip(ip_address):
    try:
        resp = requests.get('http://ip-api.com/json/%s' % ip_address).json()
    except requests.exceptions.ProxyError:
        return 'Unknown'
    return resp['city']


def update_database(data, db, key):
    db[key] = data
    logger.info('VALID KEY. Added to the database: %s\n', data)


if __name__ == '__main__':
    app.run(debug=True)
    app.auto_reload = True
