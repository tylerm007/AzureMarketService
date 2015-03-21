# AzureMarketService
Azure Market API Service 

#Espresso Logic Microservice API


```
* 1. Connect - create default server for tables, views, sprocs (security not shown)
server urlFragment=MyServer
database name = salesDB, connection = xxx
database name = hrDB, connection = yyy
* 2. Shape - create Nested Document API for multiple databases
resource name = orders, table = salesDB:order
subresource name = orders.salesRep, table = hrDB:person, attributes = names as salesRepName…
* 3. Protect - add logic
derive salesDB:customer.balance = 
            sum (orders.amountTotal where shipDate === null)
validate salesDB:customer.checkCredit msg = “yyy” where= balance > limit
* Let's run
curl get http://MyHost.com/rest/MyAccount/MyServer/v1/orders

```
