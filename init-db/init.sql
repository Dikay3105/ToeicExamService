USE toeicweb;

INSERT INTO `answers` (`id`, `questionID`, `text`, `isCorrect`) VALUES
(1, 1, 'He is eating', 0),
(2, 1, 'He is talking on the phone', 1),
(3, 1, 'He is walking', 0),
(4, 1, 'He is reading', 0),
(5, 2, 'In a conference room', 1),
(6, 2, 'In a cafe', 0),
(7, 2, 'In a park', 0),
(8, 2, 'At the airport', 0),
(9, 3, 'No parking', 0),
(10, 3, 'Stop', 0),
(11, 3, 'Do not enter', 1),
(12, 3, 'Speed limit 50', 0),
(13, 4, 'The importance of health', 0),
(14, 4, 'The importance of education', 0),
(15, 4, 'A company’s growth', 1),
(16, 4, 'The impact of climate change', 0);


INSERT INTO `history` (`id`, `userID`, `testID`, `total_Listening`, `total_Reading`, `startTime`, `endTime`) VALUES
(1, 1, 1, 75, 80, '2024-09-20 08:00:00', '2024-09-20 10:00:00'),
(2, 2, 2, 85, 90, '2024-09-21 08:30:00', '2024-09-21 10:30:00');


INSERT INTO `historydetail` (`id`, `partID`, `historyID`, `totalQuestion`, `totalCorrect`) VALUES
(1, 1, 1, 0, 0),
(2, 2, 1, 0, 0),
(3, 3, 2, 0, 0),
(4, 4, 2, 0, 0);


INSERT INTO `parts` (`id`, `name`, `number`, `description`, `testID`, `createdAt`, `updatedAt`) VALUES
(1, 'Listening', 1, 'Listening comprehension section', 1, '2024-09-23 14:09:17', '2024-09-23 14:09:17'),
(2, 'Reading', 2, 'Reading comprehension section', 1, '2024-09-23 14:09:17', '2024-09-23 14:09:17'),
(3, 'Listening', 1, 'Listening comprehension section', 2, '2024-09-23 14:09:17', '2024-09-23 14:09:17'),
(4, 'Reading', 2, 'Reading comprehension section', 2, '2024-09-23 14:09:17', '2024-09-23 14:09:17');


INSERT INTO `questions` (`id`, `partID`, `text`, `imagePath`, `imageName`, `audioPath`, `audioName`, `createdAt`, `updatedAt`, `answerCounts`) VALUES
(1, 1, 'What is the man doing?', NULL, '', 'audio1.mp3', '', '2024-09-23 14:09:17', '2024-09-23 14:09:17', 4),
(2, 1, 'Where is the meeting taking place?', NULL, '', 'audio2.mp3', '', '2024-09-23 14:09:17', '2024-09-23 14:09:17', 4),
(3, 2, 'What does the sign say?', 'image1.jpg', '', NULL, '', '2024-09-23 14:09:17', '2024-09-23 14:09:17', 4),
(4, 2, 'What is the main idea of the passage?', NULL, '', NULL, '', '2024-09-23 14:09:17', '2024-09-23 14:09:17', 4),
(5, 1, '111', 'https://res.cloudinary.com/dkw6fkmqz/image/upload/v1728466281/nvxxsg9xpe9nexmk5umm.png', '', 'https://res.cloudinary.com/dkw6fkmqz/video/upload/v1728466282/xknvwwkjlzgafqsodfkh.mp3', '', '2024-09-23 14:09:17', '2024-10-09 16:31:17', 11),
(6, 1, '111', 'https://res.cloudinary.com/dkw6fkmqz/image/upload/v1728466504/hcgezhcygsqprcrnfzns.png', '', 'https://res.cloudinary.com/dkw6fkmqz/raw/upload/v1728466506/sv7nrwplmt0fflzsyumo.mp3', '', '2024-09-23 14:09:17', '2024-10-09 16:35:02', 11),
(7, 1, '111', 'https://res.cloudinary.com/dkw6fkmqz/image/upload/v1728529922/cs5hrqqlasoquomlgubx.png', '', 'https://res.cloudinary.com/dkw6fkmqz/raw/upload/v1728529923/hngygm2kmxlbufghgluy.mp3', '', '2024-09-23 14:09:17', '2024-10-10 10:11:59', 11),
(8, 1, '111', 'https://res.cloudinary.com/dkw6fkmqz/image/upload/v1728531056/wjatqpuh9r2xowjw5k6m.png', '', 'https://res.cloudinary.com/dkw6fkmqz/raw/upload/v1728531057/kg7r6uuc3rwpffbkhbnt.mp3', '', '2024-09-23 14:09:17', '2024-10-10 10:30:53', 11),
(9, 1, '111', 'https://res.cloudinary.com/dkw6fkmqz/image/upload/v1728531761/w5zkezeh4gexf9gms2jy.png', 'profile-1.png', 'https://res.cloudinary.com/dkw6fkmqz/raw/upload/v1728531762/jxbesckuaysfd6llncsl.mp3', '002.mp3', '2024-09-23 14:09:17', '2024-09-23 14:09:17', 11);


INSERT INTO `tests` (`id`, `name`, `description`, `difficulty`, `duration`, `createdAt`, `updatedAt`) VALUES
(1, 'Test 1', 'First test for listening and reading comprehension', 'Easy', 60, '2024-09-23 14:09:17', '2024-09-23 14:09:17'),
(2, 'Test 2', 'Second test for listening and reading comprehension', 'Medium', 75, '2024-09-23 14:09:17', '2024-09-23 14:09:17'),
(3, 'Test', 'Test', 'Test', 120, '2024-10-09 08:31:25', '2024-10-09 08:31:25');


INSERT INTO `useranswers` (`id`, `userID`, `questionID`, `selectedAnswerID`, `historyID`, `isCorrect`) VALUES
(1, 1, 1, 2, 1, 1),
(2, 1, 2, 5, 1, 1),
(3, 2, 3, 11, 1, 1),
(4, 2, 4, 15, 1, 1);
