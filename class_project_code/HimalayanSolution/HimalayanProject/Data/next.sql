SELECT TOP 40 Name, Height, ClimbingStatus, FirstAscentYear FROM Peak
LEFT JOIN Expedition ON PeakID = Peak.ID
GROUP BY Name, Height, ClimbingStatus, FirstAscentYear
ORDER BY MAX(StartDate) DESC;

-- Makalu	8485	1	1955	2019-05-27
-- Everest	8850	1	1953	2019-05-19
-- Lhotse	8516	1	1956	2019-05-19
-- Chamlang	7321	1	1962	2019-05-10
-- Putha Hiunchuli	7246	1	1954	2019-05-09
-- Himlung Himal	7126	1	1992	2019-05-07
-- Lachama Chuli	6721	1	2007	2019-05-07
-- Ama Dablam	6814	1	1961	2019-05-05
-- Baruntse	7152	1	1954	2019-04-29
-- Cho Oyu	8188	1	1954	2019-04-29
-- Kangbachen	7902	1	1974	2019-04-27
-- Chobuje	6686	1	1972	2019-04-21
-- Lhotse Shar	8382	1	1970	2019-04-21
-- Kangchenjunga	8586	1	1955	2019-04-20
-- Dhaulagiri I	8167	1	1960	2019-04-17
-- Langtang Lirung	7227	1	1978	2019-04-15
-- Nuptse	7864	1	1961	2019-04-14
-- Pumori	7138	1	1962	2019-04-14
-- Himjung	7092	1	2012	2019-04-13
-- Ratna Chuli	7035	1	1996	2019-04-11
-- Sano Kailash	6452	1	2019	2019-04-05
-- Gyalzen Peak	6151	1	2019	2019-04-03
-- Annapurna I	8091	1	1950	2019-03-29
-- Jannu East	7460	0	NULL	2019-03-11
-- Chhopa Bamare	6109	1	2019	2019-02-15
-- Peri	6174	1	2016	2019-01-20
-- Manaslu	8163	1	1956	2019-01-14
-- Pasang Lhamu Chuli	7350	1	1986	2019-01-01
-- Tilicho	7134	1	1978	2018-11-25
-- Korlang Pari Tippa	5738	0	NULL	2018-11-20
-- Ghyun Himal I	6110	0	NULL	2018-11-12
-- Lachama North	6628	0	NULL	2018-11-02
-- Khangri Shar	6792	0	NULL	2018-10-30
-- Nup La Kang	6861	1	2018	2018-10-28
-- Annapurna IV	7525	1	1955	2018-10-27
-- Danphe Shail	6103	1	2017	2018-10-27
-- Sita Chuchura	6611	1	1970	2018-10-26
-- Langshisa Ri	6412	1	1982	2018-10-25
-- Saipal	7030	1	1963	2018-10-25
-- Langdung	6326	1	2017	2018-10-25