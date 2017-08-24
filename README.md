# _Hair Salon_

#### _Holds data for a hair salon including different stylists and their clients, August 22, 2017_

#### By _**Kim Schulze**_

## Description

_The user can input different stylists and their clients.  Information is stored in a database and can be accessed by different stylists._

## Setup/Installation Requirements

* _Use an up-to-date browser._

## Specifications
| Behavior | Input | Output | Reasoning |
| ---- | ---- | ---- | ---- |
| 1. Add a new stylist to system. | name: "Yvonne Renwick", stylist_id = 1 | name: "Yvonne Renwick", stylist_id = 1 | Stylists need to be created before any data is stored |
| 2. Find stylist | stylist name = "Yvonne Renwick" | stylist_id = 1 | It is important to link the stylist name with the primary key of the id |
| 3. Client class can be created to override equals. | firstClient, secondClient | true | Make sure this works with the database. |
| 4. Add new clients to a specific stylist. | stylist_name = "Yvonne Renwick", name: "Wesley Crusher", phone_number = 206-555-1701, address = "1234 Enterprise Road", id = 1 | stylist_name = "Yvonne Renwick", name: "Wesley Crusher", phone_number = 206-555-1701, address = "1234 Enterprise Road", id = 1 | Each stylist must be able to hold a client. |
| 5. Select individual stylist and see details. | stylist: "Yvonne Renwick" | stylist: "Yvonne Renwick", stylist_id = 1, clients: "Wesley Crusher", "Jean Luc Picard" |
| 6. Update a client's name. | client_name = "Beverly Crusher", "Beverly Picard" | "Beverly Picard" | It is important for employees to be able to make edits for various reasons. |
| 7. Delete a specific client. | "Wesley Crusher" | deleted | Wesley went to Star Fleet Academy |

## Known Bugs

_There are no known bugs._

## Support and contact details

_With questions contact Kim Schulze at kimberlykayschulze@gmail.com._

## Technologies Used

_Used C#, .NET, MVC, and HTML_

### License

*All Rights Reserved.  Version 1.0*

Copyright (c) 2017 **_Kim Schulze_**
