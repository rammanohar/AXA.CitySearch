Feature: US-AXA-GetCitySearch
	As a Axa insurance Front Office User
	I want to get city names and next letter,
	so that I can view the matching cities

Background:
	Given I am running in-memory configured for Test Authentication

@L1 US-CitySearch001
Scenario: GET all cities by search string and that exists returns 200 (ok)
	Given I am user 'jamier' who is a 'insurance Front Office user'
	When I 'GET' the API '/api/SmartCitySearch/CitySearch?city=bang'
	Then I receive HTTP status code '200'

@L1 US-CitySearch002
Scenario: GET all cities by search string and that exists returns 200 (ok) and next letters
	Given I am user 'jamier' who is a 'insurance Front Office user'
	When I 'GET' the API '/api/SmartCitySearch/CitySearch?city=<Key Name>'
	Then I receive HTTP status code '200'
	And the response contains '<Cities>' and '<Expected next characters>'

	Examples:
		| Key Name | Cities                   | Expected next characters |
		| BANG     | BANGUI;BANGKOK;BANGALORE | U;K;A                    |
		| LA       | LAGOS;LA PAZ;LA PLATA    | g;<space>                |

@L1 US-CitySearch003
Scenario Outline: GET all cities with invalid search string returns 400 (bad request)
	Given I am user 'jamier' who is a 'insurance Front Office user'
	When I 'GET' the API '/api/SmartCitySearch/CitySearch?city=a'
	Then I receive HTTP status code '400'
	
	
@L1 US-CitySearch004
Scenario: GET all cities by search string that does not exist returns 404 (not found)
	Given I am user 'jamier' who is a 'insurance Front Office user'
	When I 'GET' the API '/api/SmartCitySearch/CitySearch?city=ZE'
	Then I receive HTTP status code '404'
