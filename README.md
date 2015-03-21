# AzureMarketService
Azure Market API Service 

##Key Use Case for Cloud, Mobile
Virtually all organizations are looking to leverage cloud / mobile technologies, to promote internal efficiency and external access.  These require efficient networked data access to multiple databases, with enforcement of your business policy for logic and security.

Espresso creates Enterprise-class Azure RESTful APIs across multiple databases, enforcing your logic and security.  It’s declarative, so it’s remarkably fast and simple to create systems as follows.

##Rapidly Create Enterprise-class RESTful Backends
Create RESTful backends to accelerate delivering projects within your organization

1. Mobile apps: REST APIs are perfect for accessing multiple data sources.  And Espresso dramatically simplifies your apps – rich nested document APIs providing filtering, pagination, optimistic locking, generated key handling, etc.
2. Integration: RESTful APIs define business objects that can be exchanged between systems
3. Partnering: create partner relationships by exposing selected APIs publicly
4. Modernization: break down monolithic web apps, by extracting their logic into a UI-independent microservices – API Apps that centralize data access with enforcement of logic and security.  The resultant service can be shared across application architectures – not just heterogeneous web apps, but integration and partnering.
 

##Rapidly Publish Customizable API Apps to Azure
API Apps to publish into the Azure catalog.  Espresso handles the API, logic and security, Azure provides the marketplace, monitoring, and billing.  Your business ideas get to market, fast.

Even better, logic can be customized by your customers – they just add rules, and the logic is automatically re-ordered and re-optimized.  So not only do you get to market faster, your product is more flexible.
###The Difference is Declarative
Espresso stands in stark contrast to “your code goes here” frameworks, by providing a declarative approach to creating APIs, integrating databases, and enforcing logic and security.  You use a simple point and click interface to specify what you want, not the detailed coding of how to do it.  The result is remarkable business value.

 

##Time to Market
Declarative logic is 40 times more concise, since it handles all the details of change detection / propagation, SQL and transaction handling, etc.  Technology automation means you can operate at the business level, delivering faster than you ever imagined.

The API is not just access, but also the enforcement logic and integration.  Since this comprises a significant portion of a database oriented system, the savings in time and cost are remarkable.

The impact is not 10 or 15%.  Our customers are routinely reporting getting a month of work done over a weekend.

 

##Faster Maintenance Cycles
In procedural code, you spend most of your time deciphering existing code to figure out how to insert changes.  And in monolithic systems, the change unit includes the UI.

Declarative services means that logic is automatically ordered, based on system-discovered dependencies.  So you just change the logic.  Eliminate the 3 month change cycles that drive sponsors crazy, and enable Continuous Deployment.

 

##Quality
In procedural code, you not only have to ensure the logic exists, you have to analyze all the code paths to ensure its actually called.  Ensuring compliance is a daunting task.

Declarative means you don’t call the logic – the system automatically invokes the proper rules on all incoming requests.  If the rule is defined, it will be called.

It’s not just technical – Declarative helps you get the requirements right.  Business Users can read the rules, and help spot errors.  They can be engaged with the instant User Interface of Live Browser – much clearer than documents or diagrams.
Unique Declarative API App Creation
Declarative means you just declare what you want for an Enterprise-class API:

1. API: Default created instantly, then point and click Custom Resources with nesting and aliasing.  Runtime support for filtering, pagination, optimistic locking, generated keys, etc.
2. Integrate: add new data sources, use in Custom Resources
3. Enforce: logic and security with spreadsheet-like Reactive Programming expressions, plus JavaScript based on an automatically created Object Model
You also have deployment flexibility – Espresso operates on premise (VMware or Docker), or in the cloud (Azure or AWS).

##Manage your data with the Live Browser.
##Create an enterprise API like this:


See how reactive programming operates


This Fast, this Simple
This pseudo-script is all it takes to create your server.  Nothing else.  (Or, use the UI as shown in the video above.)

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

or
when running this microservice inside C#
curl http://localhost:59275/api/orders

```
