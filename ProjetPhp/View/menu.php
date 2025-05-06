<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
<nav class="navbar navbar-expand-lg navbar-light py-3" style="background-color: rgba(199, 230, 168, 0.9);">
    <div class="container">
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse justify-content-center" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item mx-3">
                    <a class="nav-link btn btn-custom" href="index.php?action=action">Accueil</a>
                </li>
                <li class="nav-item mx-3">
                    <a class="nav-link btn btn-custom" href="index.php?action=AM">Médicament</a>
                </li>
                <li class="nav-item mx-3">
                    <a class="nav-link btn btn-custom" href="index.php?action=AA">Activité</a>
                </li>
                <li class="nav-item mx-3">
                    <a class="nav-link btn btn-custom" href="index.php?action=NC">Nos Chercheurs</a>
                </li>
                <li class="nav-item mx-3">
                    <a class="nav-link btn btn-custom" href="index.php?action=AJ">Page Juridique</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
<style>
    .navbar-light .navbar-nav .nav-link {
        color: #333;
        font-weight: bold;
        font-size: 1rem;
    }

    .navbar-light .navbar-nav .nav-link:hover {
        background-color: #7ed957;
        color: white;
        border-radius: 30px;
    }

    .navbar-light .navbar-toggler-icon {
        background-color: #7ed957; 
    }

    .btn-custom {
        background-color: #7ed957;
        color: white;
        border: none;
        padding: 10px 20px;
        border-radius: 30px;
        font-size: 1rem;
        font-weight: bold;
        transition: all 0.3s ease-in-out;
    }

    .btn-custom:hover {
        background-color: #4CAF50;
        transform: translateY(-5px);
        box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
    }

    .btn-custom:active {
        transform: translateY(0);
        box-shadow: none;
    }
</style>