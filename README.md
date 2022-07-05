# SettlementBookingSystem

## Versions

`BookingController` provides an implementation using the mediator pattern and the data is stored in memory.
This implementatrion also provides unit tests.

## How to execute

1. Basic:

```
curl -X POST "https://localhost:44355/Booking" -H  "accept: text/plain" -H  "Content-Type: application/json" -d "{\"name\":\"dfsfs\",\"bookingTime\":\"10:15\"}"
```
