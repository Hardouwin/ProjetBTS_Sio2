<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Mention Légal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
<div class="jumbotron" style="background-color: rgba(199, 230, 168, 0.9); color: black; height: 150px;">
    <div class="d-flex align-items-center justify-content-between">
        <!-- Logo -->
        <img src="View/logo.png" alt="logo" style="height: 100px; margin-right: 15px;">
        <!-- Titre -->
        <h1 class="mx-auto" style="margin: 0;">Bienvenue sur GSB !</h1>
    </div>
</div>



<?php
include('menu.php');
?>






<div class="container mt-5" style="padding: 1rem;">
    <div class="row">
        <div class="col-12">
            <div class="card" style="background-color: #f8f9fa; padding: 2rem;">
                <h2 class="text-center mb-4">Mentions Légales</h2>
                <p><strong>Éditeur du site :</strong><br>
                GSB - Grande Société de Biopharmacie<br>
                Siège social : Cugnaux<br>
                Téléphone : 06 01 02 03 04<br>
                Email : gsb@gmail.com</p>

                <p><strong>Directeur de publication :</strong><br>
                Mr X</p>

                <p><strong>Hébergement :</strong><br>
                OVH - [ovh@gmail.com<br>
                Téléphone : 07 01 02 03 04</p>

                <p><strong>Activité :</strong><br>
                GSB est une entreprise spécialisée dans la recherche, la fabrication et la distribution de médicaments. Elle propose également des activités et des événements liés à son domaine d’expertise.</p>

                <p><strong>Propriété intellectuelle :</strong><br>
                Tous les contenus présents sur ce site (textes, images, logos, vidéos, etc.) sont protégés par le droit d’auteur et la propriété intellectuelle. Toute reproduction ou utilisation sans autorisation préalable est interdite.</p>

                <p><strong>Protection des données :</strong><br>
                Conformément au RGPD, les informations collectées sur ce site sont utilisées uniquement dans le cadre des services proposés par GSB. Vous avez un droit d’accès, de rectification et de suppression de vos données personnelles en contactant [Adresse email].</p>

                <p><strong>Responsabilité :</strong><br>
                GSB met tout en œuvre pour assurer l'exactitude des informations diffusées sur ce site, mais ne saurait être tenue responsable d'éventuelles erreurs ou omissions.</p>
            </div>
        </div>
    </div>
</div>



    <!-- Footer -->
    <footer class="bg-light text-center p-3 mt-5">
        <p>&copy; 2025 GSB. Projet Php.</p>
    </footer>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
