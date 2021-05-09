INSERT INTO [Keyword] ([name]) VALUES 
	('New iCollection'),
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
	('Collection1', 1, '1', '05/29/2015 5:50 AM', 'gallery_environment'),
	('My Fish', 1, '1', '05/28/2015 5:50 AM', 'Ocean_environment'),
	('My Beer', 1, '1', '05/27/2015 5:50 AM', 'gallery_environment'),
	('My Tools', 1, '1', '05/26/2015 5:50 AM', 'Ocean_environment'),
	('My Friends', 1, '1', '05/25/2015 5:50 AM', 'gallery_environment'),
	('Collection2', 1, '2', '05/24/2015 5:50 AM', 'Ocean_environment'),
	('My Trophyies', 1, '2', '05/23/2015 5:50 AM', 'gallery_environment'),
	('My Plants', 1, '2', '05/22/2015 5:50 AM', 'Ocean_environment'),
	('My Cards', 1, '2', '05/21/2015 5:50 AM', 'gallery_environment'),
	('My Games', 1, '2', '05/20/2015 5:50 AM', 'Ocean_environment'),
	('Collection3', 1, '3', '05/19/2015 5:50 AM', 'gallery_environment'),
	('My Stickers', 1, '3', '05/18/2015 5:50 AM', 'Ocean_environment'),
	('My Stamps', 1, '3', '05/17/2015 5:50 AM', 'gallery_environment'),
	('My Posters', 1, '3', '05/16/2015 5:50 AM', 'Ocean_environment'),
	('My Funco Pops', 1, '3', '05/15/2015 5:50 AM', 'gallery_environment'),
	('Collection4', 1, '4', '05/14/2015 5:50 AM', 'Ocean_environment'),
	('My Toenails', 1, '4', '05/13/2015 5:50 AM', 'gallery_environment'),
	('My Furniture', 1, '4', '05/12/2015 5:50 AM', 'Ocean_environment'),
	('My statues', 1, '4', '05/11/2015 5:50 AM', 'gallery_environment'),
	('My Vases', 1, '4', '05/10/2015 5:50 AM', 'Ocean_environment');





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


	INSERT INTO [CollectionKeyword] (collect_id, keyword_id, date_added) VALUES
	(1,2, '12/29/2012 8:55 AM'),
	(2,3, '01/25/2015 12:42 AM'),
	(3,4, '04/26/2017 4:51 AM'),
	(4,1, '06/28/2018 3:33 AM'),
	(5,10, '07/22/2019 1:24 AM'),
	(6,5, '09/24/2018 6:28 AM'),
	(7,11, '03/19/2014 2:59 AM'),
	(8,7, '04/14/2016 3:18 AM'),
	(9,9, '06/26/2011 6:26 AM'),
	(10,8, '09/28/2010 2:43 AM'),
	(11,3, '04/22/2014 7:19 AM'),
	(12,2, '02/24/2013 11:21 AM'),
	(13,11, '01/26/2017 6:45 AM'),
	(14,13, '03/28/2019 8:36 AM'),
	(15,3, '10/22/2013 10:44 AM'),
	(16,12, '11/24/2014 9:36 AM'),
	(17,9, '12/19/2015 3:28 AM'),
	(18,4, '01/14/2019 3:47 AM'),
	(19,6, '06/14/2020 11:56 AM'),
	(20,4, '08/13/2011 10:14 AM');
