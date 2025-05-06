<?php
include ("db_connect.php");
$_request_method = $_SERVER["REQUEST_METHOD"];
switch($_request_method)
{
    case 'GET' : 
        if(!empty($_GET["id"]))
        {
            $id = intval($_GET["id"]);
            getAct($id);
        }
        else
        {
            getActs();
        }
        break;

    case 'POST':
        AddInscription();
        break;

    // case 'PUT':
    //     $id = intval($_GET["id"]);
    //     updateUsers($id);
    //     break;

    // case 'DELETE':
    //     $id = intval($_GET["id"]);
    //     deleteUsers($id);
    //     break;
default :
header("HTTP/1.0 405 Methid Not Allowed");
break;

}

function getActs()
{
    global $conn;
    $query = "SELECT * FROM activitées";
    $response =array();

    $conn->query("SET NAMES utf8");
    $result= $conn ->query($query);
    while ($row = $result->fetch())
    {
        $response[] = $row;
    }
$result->closeCursor();
header('Content-Type: application/json');
echo json_encode($response, JSON_PRETTY_PRINT);

}
function getAct($id=0)
{
    global $conn;
    $query = "SELECT * FROM activitées";
    if ($id !=0)
    {
        $query= "WHERE idA=".$id." LIMIT 1";
    }
$conn->query("SET NAMES UTF8").
$result = $conn->query($query);
while ($row = $result->fetch())
{
    $response[]= $row;

}
header('Content-Type: application/json');
echo json_encode($response, JSON_PRETTY_PRINT);

}


function AddInscription()
{
    global $conn;
    $nom = $_POST["nom"];
    $prenom = $_POST["prenom"]; 
    $mail = $_POST["mail"];
    $idA = $_POST["idA"];
    date_default_timezone_set("UTC");
    $date = date("Y-m-d H:i:s");

    $query = "INSERT INTO inscription(nom, prenom, mail, date_Ins, idA) VALUES('$nom', '$prenom', '$mail','$date','$idA')";

    $conn->query("SET NAMES UTF8");

    if ($conn->query($query)) {
        $response = array(
            'status' => 1,
            'status_message' => 'Inscription mise à jour avec succès.'
        );
    } else {
        $response = array(
            'status' => 0,
            'status_message' => 'Échec de la mise à jour.'
        );
    }

    header('Content-Type: application/json');
    echo json_encode($response);
}

function AddIns()
{
    global $conn;
    $
    $nom = $_POST["nom"];
    $prenom = $_POST["prenom"];  // Ajouté pour correspondre à ta table utilisateurs
    $mail = $_POST["mail"];

    // Correction de la requête SQL (manquait une virgule)
    $query = "INSERT INTO utilisateurs(nom, prenom, mail) VALUES('$nom', '$prenom', '$mail')";

    $conn->query("SET NAMES UTF8");

    if ($conn->query($query)) {
        $response = array(
            'status' => 1,
            'status_message' => 'Inscription mise à jour avec succès.'
        );
    } else {
        $response = array(
            'status' => 0,
            'status_message' => 'Échec de la mise à jour.'
        );
    }

    header('Content-Type: application/json');
    echo json_encode($response);
}

