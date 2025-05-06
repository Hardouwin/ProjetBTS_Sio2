<?php
function addUsers()
{
    $nom = $_POST["nom"];
    $prenom = $_POST["prenom"];
    $mail = $_POST["mail"];
    insertUser($nom,$prenom,$mail);

    $activity = selectAct();
    include("view/afficheActi.php");
    
}


function addAct()
{
    $nom = $_POST["nom"];
    $prenom = $_POST["prenom"];
    $mail = $_POST["mail"];
    date_default_timezone_set("UTC");
    $date = date("Y-m-d H:i:s");
    $idA = $_POST["idA"];
    insertAct($nom,$prenom,$mail,$date,$idA);

    $activity = selectAct();
    include("view/afficheActi.php");
    
}


function getAllUsers()
{
    include("view/pageAcc.php");

}

function afficheJuridique()
{
    include("view/pageLeg.php");

}

function getAllMedic()
{
    $medic = selectUsers();
    include("view/afficheMed.php");
}

function getAllAct()
{
    
    $activity = selectAct();
    include("view/afficheActi.php");
}

function getEffet()
{   
    $id1 = $_POST["id1"];
    $id2 = $_POST["id2"];
    
    $effet = selectEffet($id1, $id2); 
    $medic = selectUsers();
    
    include("view/afficheMed.php");
}

function afficheChercheur()
{
    $activity = selectCher();
    include("view/afficheCher.php");

}

function addChercheur()
{
    $nom = $_POST["nom"];
    $prenom = $_POST["prenom"];
    $mail = $_POST["mail"];
    date_default_timezone_set("UTC");
    $date = date("Y-m-d H:i:s");
    $idA = $_POST["idA"];
    insertCher($nom,$prenom,$mail,$date,$idA);

    $activity = selectCher();
    include("view/afficheCher.php");
    
}

?>