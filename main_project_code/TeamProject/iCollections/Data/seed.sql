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






INSERT INTO [FriendsWith](user1_id, user2_id, began) VALUES
	('1','2', '05/29/2015 5:50 AM'),
	('1','4', '05/25/2015 5:50 AM'),
	('2','1', '05/29/2015 5:50 AM'),
	('2','3', '05/23/2015 5:50 AM'),
	('3','2', '05/23/2015 5:50 AM'),
	('3','4', '05/21/2015 5:50 AM'),
	('4','1', '05/25/2015 5:50 AM'),
	('4','3', '05/21/2015 5:50 AM');

	INSERT INTO [CollectionKeyword](collect_id, keyword_id, date_added) VALUES
	(1,2, '05/29/2015 5:50 AM'),
	(1,3, '05/29/2015 5:50 AM'),
	(2,5, '05/29/2015 5:50 AM');