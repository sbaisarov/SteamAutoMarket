import sys
import uuid
import shelve


def generate(subscription_time, amount=1):
	keys = []
	with shelve.open("database/clients", writeback=True) as db, \
			open("database/keys/%s months.txt" % (subscription_time / 30), "a+") as f:
		for _ in range(amount):
			key = str(uuid.uuid4())
			db.setdefault(key, {"subscription_time": subscription_time, "devices": {}})
			f.write(key + "\n")
			keys.append(key)
			
	return keys


if __name__ == '__main__':
	amount = sys.argv[1]
	subscription_time = int(sys.argv[2])
	generate(subscription_time, amount)