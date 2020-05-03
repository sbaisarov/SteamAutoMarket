import sys
import uuid
import shelve


def generate(subscription_time, amount=1):
	keys = []
	with shelve.open("database/clients", writeback=True) as db, \
			open("database/keys/%s months.txt" % (subscription_time // 30), "a+") as f:
		clients = db["clients"]
		for _ in range(amount):
			key = str(uuid.uuid4())
			clients.setdefault(key, {"subscription_time": subscription_time, "devices": {}, "payments": []})
			f.write(key + "\n")
			keys.append(key)

	if amount == 1:
		return keys[0]

	return keys


if __name__ == '__main__':
	amount = int(sys.argv[1])
	subscription_time = int(sys.argv[2])
	keys = generate(subscription_time, amount)
	print(keys)