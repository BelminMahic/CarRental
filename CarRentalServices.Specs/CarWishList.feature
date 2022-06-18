Feature: CarWishList
	User wants to add certain cars to a wish list



Scenario: Add car to wish list
	Given a user 'JohnDoe' is logged in
	And car is selected
    | Id | Brand  | Model   | ImageUrl           | PricePerDay | Location | IsAvailable |
	| 2  | Audi   | A6      | https://localhost/ | 20          | Berlin   | true        |
	When the customer adds car '2' to wish list
	Then a selected car is added to wish list
	