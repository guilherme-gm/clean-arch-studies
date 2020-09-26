CREATE SCHEMA `clean_arch_app` ;

CREATE TABLE `clean_arch_app`.`users` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `role` ENUM('NormalUser', 'Moderator') NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC));

CREATE TABLE `clean_arch_app`.`questions` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(100) NOT NULL,
  `message` TEXT NOT NULL,
  `asker` BIGINT NOT NULL,
  `correctAnswer` BIGINT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `asker_idx` (`asker` ASC),
  CONSTRAINT `asker_fk`
    FOREIGN KEY (`asker`)
    REFERENCES `clean_arch_app`.`users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  INDEX `correctAnswer_idx` (`correctAnswer` ASC),
  CONSTRAINT `correctAnswer_fk`
    FOREIGN KEY (`correctAnswer`)
    REFERENCES `clean_arch_app`.`answers` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `clean_arch_app`.`answers` (
  `id` BIGINT NOT NULL,
  `author` BIGINT NOT NULL,
  `question` BIGINT NOT NULL,
  `message` TEXT NULL,
  PRIMARY KEY (`id`),
  INDEX `index2` (`question` ASC),
  INDEX `fk_answers_author_idx` (`author` ASC),
  CONSTRAINT `fk_answer_question`
    FOREIGN KEY (`question`)
    REFERENCES `clean_arch_app`.`questions` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_answers_author`
    FOREIGN KEY (`author`)
    REFERENCES `clean_arch_app`.`users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


