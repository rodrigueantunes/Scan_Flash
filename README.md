# Scan_Flash - Outil de Capture et de Simulation de Code-barres

## Description

**Scan_Flash** est une application puissante et intuitive qui permet de scanner des codes-barres à partir de plusieurs écrans, en offrant des fonctionnalités avancées telles que la capture d'écran complète ou la sélection manuelle d'une zone d'intérêt. Grâce à son interface simple et à ses fonctionnalités de simulation de saisie, Scan_Flash permet d'intégrer les résultats des scans directement dans des applications telles que des éditeurs de texte, des systèmes de gestion de contenu ou tout autre logiciel supportant la saisie au clavier.

Développée avec **.NET** et une combinaison de bibliothèques puissantes comme **ZXing.Net** pour le décodage de codes-barres et **InputSimulator** pour la simulation de saisie clavier, cette application est idéale pour les professionnels qui cherchent une solution rapide et fiable pour gérer les codes-barres dans leur environnement de travail.

## Fonctionnalités

- **Sélection dynamique de l'écran** : Scan_Flash détecte automatiquement les écrans connectés et vous permet de sélectionner celui que vous souhaitez utiliser pour la capture.
- **Capture d'écran complète ou partielle** : Capturez l'intégralité de l'écran ou définissez une zone spécifique à scanner.
- **Lecture de 20 formats de codes-barres** : Prise en charge des formats les plus populaires comme DataMatrix, QR Code, Code 128, et bien plus encore.
- **Simulation de saisie clavier** : Après avoir détecté un code-barres, l'application simule automatiquement la saisie du texte dans l'application active (par exemple, Notepad, Excel, etc.).
- **Interface simple et efficace** : L'interface de Scan_Flash est conçue pour être intuitive, avec une configuration minimale nécessaire pour démarrer.
- **Prise en charge multi-écrans** : Sélectionnez rapidement entre plusieurs écrans pour adapter la capture d'écran à vos besoins.

## Installation

### Prérequis
- **.NET Framework 8** ou supérieur.
- **Windows 10** ou version supérieure pour la gestion multi-écrans.
- **Bibliothèque ZXing.Net** pour la gestion des codes-barres.
- **Bibliothèque InputSimulator** pour la simulation de la saisie clavier.

### Étapes d'installation

1. Télécharger la Release
2. Exécuter le programme

## Utilisation

1. **Lancer l'application** : Démarrez l'application et sélectionnez l'écran que vous souhaitez utiliser pour la capture.
2. **Sélectionner une zone** : Cliquez et faites glisser la souris pour délimiter une zone sur l'écran. L'application détectera automatiquement les codes-barres dans cette zone.
3. **Capturer l'écran complet** : Si vous préférez scanner l'intégralité de l'écran, utilisez l'option de capture complète pour analyser tous les codes-barres présents.
4. **Simuler la saisie** : Dès qu'un code-barres est détecté, le texte associé sera automatiquement simulé dans l'application active.

## Technologies Utilisées

- **.NET Framework 8** : Plateforme de développement robuste pour créer des applications Windows natives.
- **ZXing.Net** : Une bibliothèque open-source de décodage de codes-barres qui permet de lire une grande variété de formats.
- **InputSimulator** : Permet de simuler une saisie au clavier dans les applications Windows de manière simple et fiable.
- **Windows API** : Utilisation des fonctions natives de Windows pour gérer la capture d'écran, l'enregistrement des raccourcis clavier et la gestion des fenêtres.

## Version

- **V0.1** - Première version de Scan_Flash, avec simulation de saisie clavier.

## Contributions

Les contributions sont les bienvenues ! Si vous souhaitez ajouter des fonctionnalités, corriger des bogues ou améliorer la documentation, n'hésitez pas à créer une **pull request**. Veuillez consulter notre **[contribution guide](CONTRIBUTING.md)** pour plus de détails.

## Auteurs

- **[Antunes Rodrigue]** - Créateur principal et développeur.

## Licence

Ce projet est sous licence **MIT**. 
