INSERT INTO [Keyword] ([name]) VALUES 
	('Sports'),
	('Cards'),
	('Fish'),
	('Tools'),
	('Art'),
	('Models'),
	('Boardgames'),
	('Stamps'),
	('Insects'),
	('Hats'),
	('Shoes'),
	('Games'),
	('Trophies');

INSERT INTO [Follow] (follower,followed, began) VALUES
	(1,2, '05/29/2015 5:50 AM'),
	(1,3, '05/25/2015 5:50 AM'),
	(1,4, '05/26/2015 5:50 AM'),
	(2,1, '05/28/2015 5:50 AM'),
	(2,3, '05/22/2015 5:50 AM'),
	(3,2, '05/24/2015 5:50 AM'),
	(3,4, '05/19/2015 5:50 AM'),
	(4,1, '05/14/2015 5:50 AM'),
	(4,3, '05/13/2015 5:50 AM');


INSERT INTO [Collection](name,visibility,user_id,date_made, route) VALUES
	('Collection1', 1, '1', '05/29/2015 5:50 AM', '~/ocean_environment'),
	('My Fish', 1, '1', '05/28/2015 5:50 AM', '~/ocean_environment'),
	('My Beer', 1, '1', '05/27/2015 5:50 AM', '~/ocean_environment'),
	('My Tools', 1, '1', '05/26/2015 5:50 AM', '~/ocean_environment'),
	('My Friends', 1, '1', '05/25/2015 5:50 AM', '~/ocean_environment'),
	('Collection2', 1, '2', '05/24/2015 5:50 AM', '~/ocean_environment'),
	('My Trophyies', 1, '2', '05/23/2015 5:50 AM', '~/ocean_environment'),
	('My Plants', 1, '2', '05/22/2015 5:50 AM', '~/ocean_environment'),
	('My Cards', 1, '2', '05/21/2015 5:50 AM', '~/ocean_environment'),
	('My Games', 1, '2', '05/20/2015 5:50 AM', '~/ocean_environment'),
	('Collection3', 1, '3', '05/19/2015 5:50 AM', '/gallery_environment'),
	('My Stickers', 1, '3', '05/18/2015 5:50 AM', '/gallery_environment'),
	('My Stamps', 1, '3', '05/17/2015 5:50 AM', '/gallery_environment'),
	('My Posters', 1, '3', '05/16/2015 5:50 AM', '/gallery_environment'),
	('My Funco Pops', 1, '3', '05/15/2015 5:50 AM', '/gallery_environment'),
	('Collection4', 1, '4', '05/14/2015 5:50 AM', '/gallery_environment'),
	('My Toenails', 1, '4', '05/13/2015 5:50 AM', '/gallery_environment'),
	('My Furniture', 1, '4', '05/12/2015 5:50 AM', '/gallery_environment'),
	('My statues', 1, '4', '05/11/2015 5:50 AM', '/gallery_environment'),
	('My Vases', 1, '4', '05/10/2015 5:50 AM', '/gallery_environment');


INSERT INTO [Photo](name, user_id, date_uploaded) VALUES
	('new beer','1', '05/29/2015 5:50 AM'),
	('new trophy','1', '05/29/2015 5:50 AM'),
	('best plant','1', '05/29/2015 5:50 AM'),
	('newest stuff','2', '05/29/2015 5:50 AM'),
	('Images 3876','2', '05/29/2015 5:50 AM'),
	('WhiteTail Carp','2', '05/29/2015 5:50 AM'),
	('The hulk','3', '05/29/2015 5:50 AM'),
	('This photo has a really long name','3', '05/29/2015 5:50 AM'),
	('CIgar Box','3', '05/29/2015 5:50 AM'),
	('Energy Drink Can','4', '05/29/2015 5:50 AM'),
	('Weird Large Bug','4', '05/29/2015 5:50 AM'),
	('Deer','4', '05/29/2015 5:50 AM'),
	('Other stuff','4', '05/29/2015 5:50 AM'),
	('pictures of things!','4', '05/29/2015 5:50 AM');

INSERT INTO [FriendsWith](user1_id, user2_id, began) VALUES
	('1','2', '05/29/2015 5:50 AM'),
	('1','4', '05/25/2015 5:50 AM'),
	('2','1', '05/29/2015 5:50 AM'),
	('2','3', '05/23/2015 5:50 AM'),
	('3','2', '05/23/2015 5:50 AM'),
	('3','4', '05/21/2015 5:50 AM'),
	('4','1', '05/25/2015 5:50 AM'),
	('4','3', '05/21/2015 5:50 AM');