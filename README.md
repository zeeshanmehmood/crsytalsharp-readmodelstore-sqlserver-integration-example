# Crystal Sharp - Read Model Store with Microsoft SQL Server Example
Crystal Sharp framework - `Read Model Store` integration code example with `Microsoft SQL Server`.


### About This Example
This example uses `EventStoreDB` for storing events and `Microsoft SQL Server` for storing read models.

`Command Handlers` have been used to store the data in the event store.

`Event Handlers` have been used to store, update, and delete the data in the read model store.

`Query Handlers` have been used to retrieve the data from the read model store.


### How to Run

* Change the event store connectionstring in `appsettings.json` file.
* Change the read model store connectionstring in `appsettings.json` file and then run the `Migrations`.
* Run the `WebAPI` project.
