Feature: CarReservations
	As a car rental customer
	I want to be independent of a car during my travels
	So I need to be able to rent a car that suits my needs

Scenario: Reserving an available car
	Given a logged in customer 'JohnDoe'
	And and a available car
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	When the customer 'John Doe' reserves the car '1' from '5/28/2021' to '5/31/2021'
	Then the car should be successfully reserved
	And the car should not be available for rental any more
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | false        |

Scenario: Reserving an unavailable car
	Given a logged in customer 'JohnDoe'
	And an a car that has allready been reserved
	| Id | Brand     | Model   | ImageUrl           | PricePerDay | Location   | IsAvailable |
	| 4  | Merecedes | A1      | https://localhost/ | 10          | Dusseldorf | false       |
	When the customer tries to reserve that '4'
	Then a customer friendly message is displayed

Scenario: Viewing current personal reservations
	Given a logged in customer 'JohnDoe'
	And the customer is currently renting
	| Id | CustomerId | CarId | RentFrom  | RentTo    |
	| 1  | JohnDoe    | 4     | 6/01/2021 | 9/10/2021 | 
	When the customer requests overview of current reservations
	Then the customer should see a list of current reservations
 

Scenario: Viewing historical personal reservations
	Given a logged in customer 'JohnDoe'
	And the customer has previously rented
	| Id | CustomerId | CarId | RentFrom  | RentTo    |
	| 1  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 2  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 3  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 | 
	When the customer requests reservations history
	Then the customer should see correct historical transactions
	| Id | CustomerId | CarId | RentFrom  | RentTo    |
	| 1  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 2  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 3  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |

Scenario: Returning a rented car
	Given a rented car
	| Id | Brand     | Model | ImageUrl           | PricePerDay | Location   | IsAvailable |
	| 4  | Merecedes | A1    | https://localhost/ | 10          | Dusseldorf | false       |
	When the car is returned
	Then the car should be available for renting again