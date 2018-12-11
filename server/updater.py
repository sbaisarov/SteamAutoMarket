import shelve

with shelve.open("database/clients", writeback=True) as db:
	clients = db["clients"]
	for key in clients:
		client = clients[key]
		active_devices = client["devices"]
		for device in active_devices:
		    if client["subscription_time"] != 0:
			    client["subscription_time"] -= 1
		if not active_devices:
			if client["subscription_time"] != 0:
			    client["subscription_time"] -= 1
		active_devices.clear()