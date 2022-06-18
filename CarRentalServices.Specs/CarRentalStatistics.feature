Feature: CarRentalStatistics
	As a customer that don't know anything about cars
	I am interested getting value for money
	And need an indicator on what cars most customers are satisfied with

Scenario: Get top three most popular cars
	Given rental history
	| Id | CustomerId | CarId | RentFrom  | RentTo    |
	| 1  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 2  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 3  | JohnDoe    | 4     | 5/28/2020 | 5/31/2020 |
	| 4  | JohnDoe    | 1     | 5/28/2020 | 5/31/2020 |
	| 5  | JaneDoe    | 2     | 5/28/2020 | 5/31/2020 |
	| 6  | JaneDoe    | 2     | 5/28/2020 | 5/31/2020 |
	When displaying most popular cars
	Then the list should be weighted towards brands most rented 
