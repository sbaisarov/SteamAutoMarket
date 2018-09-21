import shelve

with shelve.open("database/clients", writeback=True) as db:
	for key in db:
		client = db[key]
		active_devices = client["devices"]
		for device in active_devices:
			client["subscription_time"] -= 1
		active_devices.clear()