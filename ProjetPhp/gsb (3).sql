-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : jeu. 20 mars 2025 à 10:34
-- Version du serveur : 8.3.0
-- Version de PHP : 8.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gsb`
--

-- --------------------------------------------------------

--
-- Structure de la table `activitées`
--

DROP TABLE IF EXISTS `activitées`;
CREATE TABLE IF NOT EXISTS `activitées` (
  `idA` varchar(50) NOT NULL,
  `nom` varchar(50) DEFAULT NULL,
  `description` varchar(50) DEFAULT NULL,
  `date_activite` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idA`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `activitées`
--

INSERT INTO `activitées` (`idA`, `nom`, `description`, `date_activite`) VALUES
('A1', 'Randonnée', 'Balade en montagne', '2025-04-10'),
('A2', 'Yoga', 'Séance de relaxation', '2025-04-12'),
('A3', 'Atelier cuisine', 'Cuisine méditerranéenne', '2025-04-15');

-- --------------------------------------------------------

--
-- Structure de la table `inscription`
--

DROP TABLE IF EXISTS `inscription`;
CREATE TABLE IF NOT EXISTS `inscription` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) DEFAULT NULL,
  `prenom` varchar(50) DEFAULT NULL,
  `mail` varchar(50) DEFAULT NULL,
  `date_Ins` date DEFAULT NULL,
  `idA` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `idA` (`idA`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `inscription`
--

INSERT INTO `inscription` (`Id`, `nom`, `prenom`, `mail`, `date_Ins`, `idA`) VALUES
(1, 'Dupont', 'Jean', 'jean.dupont@email.com', '2025-03-18', 'A1'),
(2, 'Martin', 'Sophie', 'sophie.martin@email.com', '2025-03-19', 'A2'),
(3, 'Leclerc', 'Paul', 'paul.leclerc@email.com', '2025-03-20', 'A1'),
(4, 'Durand', 'Claire', 'claire.durand@email.com', '2025-03-21', 'A3'),
(6, 'tut', 'yry', 'mdr@gmail.fr', '2025-03-20', ''),
(7, 'IphoneX', 'lo', 'lol', '2025-03-12', 'A1'),
(8, 'm', 'y', 'y', '2025-03-12', 'A'),
(9, 'Albert Einsteinh', 'Neymarhh', 'albert.einstein@example.com', '2025-03-20', ''),
(10, 'az', 'az', 'az', '0000-00-00', 'B');

-- --------------------------------------------------------

--
-- Structure de la table `interaction`
--

DROP TABLE IF EXISTS `interaction`;
CREATE TABLE IF NOT EXISTS `interaction` (
  `id` varchar(50) NOT NULL,
  `id_1` varchar(50) NOT NULL,
  PRIMARY KEY (`id`,`id_1`),
  KEY `id_1` (`id_1`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `interaction`
--

INSERT INTO `interaction` (`id`, `id_1`) VALUES
('M1', 'M2'),
('M1', 'M5'),
('M2', 'M3'),
('M3', 'M4');

-- --------------------------------------------------------

--
-- Structure de la table `medicaments`
--

DROP TABLE IF EXISTS `medicaments`;
CREATE TABLE IF NOT EXISTS `medicaments` (
  `id` varchar(50) NOT NULL,
  `nom` varchar(50) DEFAULT NULL,
  `effet_t` varchar(50) DEFAULT NULL,
  `effet_s` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `medicaments`
--

INSERT INTO `medicaments` (`id`, `nom`, `effet_t`, `effet_s`) VALUES
('M1', 'Paracétamol', 'Antidouleur', 'Somnolence'),
('M2', 'Ibuprofène', 'Anti-inflammatoire', 'Irritation gastrique'),
('M3', 'Aspirine', 'Anticoagulant', 'Maux d’estomac'),
('M4', 'Amoxicilline', 'Antibiotique', 'Allergie possible'),
('M5', 'Doliprane', 'Antalgique', 'Fatigue légère');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
