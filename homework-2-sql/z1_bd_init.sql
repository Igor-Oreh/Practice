DROP TABLE IF EXISTS QuizTeam CASCADE;
DROP TABLE IF EXISTS QuizQuizmaster CASCADE;
DROP TABLE IF EXISTS TeamMember CASCADE;
DROP TABLE IF EXISTS Answer CASCADE;
DROP TABLE IF EXISTS Quizmaster CASCADE;
DROP TABLE IF EXISTS Member CASCADE;
DROP TABLE IF EXISTS Team CASCADE;
DROP TABLE IF EXISTS Quiz CASCADE;
DROP TABLE IF EXISTS Result CASCADE;
DROP TABLE IF EXISTS Question CASCADE;
DROP TABLE IF EXISTS "User" CASCADE;

DROP TYPE IF EXISTS gender CASCADE;
DROP TYPE IF EXISTS difficulty CASCADE;

CREATE TYPE gender AS ENUM ('male', 'female');
CREATE TYPE difficulty AS ENUM ('easy', 'medium', 'hard');

CREATE TABLE IF NOT EXISTS "User" (
    user_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    age INT,
    sex gender,
    phonenumber VARCHAR(13), -- Посчитал число цифр в номерах
    email VARCHAR(50)    
);

CREATE TABLE IF NOT EXISTS Result (
    result_id SERIAL PRIMARY KEY,
    place INT,
    score INT
);

CREATE TABLE IF NOT EXISTS Team (
    team_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS Quiz (
    quiz_id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    quiz_date TIMESTAMP DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS Question (
    question_id SERIAL PRIMARY KEY,
	quiz_id INT REFERENCES Quiz(quiz_id) ON DELETE CASCADE,
	question_number INT NOT NULL,
    question_text VARCHAR(200) NOT NULL,
    difficulty difficulty,
    score INT NOT NULL
);

CREATE TABLE IF NOT EXISTS Quizmaster (
    quizmaster_id INT PRIMARY KEY REFERENCES "User"(user_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Member (
    member_id INT PRIMARY KEY REFERENCES "User"(user_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Answer (
    answer_id SERIAL PRIMARY KEY,
    question_id INT NOT NULL REFERENCES Question(question_id) ON DELETE CASCADE,
    body VARCHAR(50) NOT NULL,
    is_correct BOOL NOT NULL
);

CREATE TABLE IF NOT EXISTS TeamMember (
    team_id INT REFERENCES Team(team_id) ON DELETE CASCADE,
    member_id INT REFERENCES Member(member_id) ON DELETE CASCADE,
    PRIMARY KEY(team_id, member_id)
);

CREATE TABLE IF NOT EXISTS QuizTeam (
    quiz_id INT REFERENCES Quiz(quiz_id) ON DELETE CASCADE,
    team_id INT REFERENCES Team(team_id) ON DELETE CASCADE,
    result_id INT REFERENCES Result(result_id),
    PRIMARY KEY(quiz_id, team_id)
);

CREATE TABLE IF NOT EXISTS QuizQuizmaster (
    quiz_id INT REFERENCES Quiz(quiz_id) ON DELETE CASCADE,
    quizmaster_id INT REFERENCES Quizmaster(quizmaster_id) ON DELETE CASCADE,
    PRIMARY KEY(quiz_id, quizmaster_id)
);
