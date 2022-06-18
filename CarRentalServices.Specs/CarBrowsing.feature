Feature: CarBrowsing
	As a car rental customer
	I want to be able to choose what car to rent
	By browsing a selection of available cars for


Scenario: Customer can display list of all available cars
	Given a logged in customer 'JohnDoe'
	And a list of available cars
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    | true        |
	When the customer requests available cars
	Then the result should be
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    | true        |

Scenario: Customer should not see unavaible cars
	Given a logged in customer 'JohnDoe'
	And a list of available cars
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    | true        |
	But these cars are taken
	| Id | Brand     | Model   | ImageUrl           | PricePerDay | Location   | IsAvailable |
	| 4  | Merecedes | A1      | https://localhost/ | 10          | Dusseldorf | false       |
	| 5  | Skoda     | Octavia | https://localhost/ | 10          | Skopje     | false       |
	When the customer requests available cars
    Then the result should be
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    | true        |

Scenario: Customers can filter cars by location
	Given a logged in customer 'JohnDoe'
	And a list of available cars
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    |
	When the customer requests available cars in 'Berlin'
	Then the result should be
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   |


Scenario: Customers can filter cars by price range
	Given a logged in customer 'JohnDoe'
	And a list of available cars
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    |
	When the customer requests car within price range of 15
	Then the result should be
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    |


Scenario: Car prices rising on weekends
	Given  a list of all cars
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 10          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 15          | Tokyo    | true        |
	| 4  | Merecedes | A1      | https://localhost/ | 10         | Dusseldorf | false       |
	| 5  | Skoda     | Octavia | https://localhost/ | 10          | Skopje     | false       |
	When today '06/11/2021' is friday, saturday or sunday
	Then the prices for all cars are increased for 15%
	| Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 1  | Toyota | Auris   | https://localhost/ | 11.5          | Kobe     | true        |
	| 2  | Audi   | A6      | https://localhost/ | 23          | Berlin   | true        |
	| 3  | Nissan | X-Trail | https://localhost/ | 17.25         | Tokyo    | true        |
	| 4  | Merecedes | A1      | https://localhost/ | 11.5        | Dusseldorf | false       |
	| 5  | Skoda     | Octavia | https://localhost/ | 11.5        | Skopje     | false       |