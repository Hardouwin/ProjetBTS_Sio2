<?php
include ("db_connect.php");
$_request_method = $_SERVER["REQUEST_METHOD"];
switch($_request_method)
{
    case 'GET' : 
        if (!empty($_GET["id"])) {
            $id = intval($_GET["id"]);
            getMedic($id);
        } 
        else {
            getMedics();
        }
        break;

     case 'POST':
         getEffet();
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

function getMedics()
{
    global $conn;
    $query = "SELECT * FROM medicaments";
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
function getMedic($id=0)
{
    global $conn;
    $query = "SELECT * FROM medicaments";
    if ($id !=0)
    {
        $query= "WHERE id=".$id." LIMIT 1";
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

function getEffet()
{
    global $conn;
    if (!isset($_POST["id1"]) || !isset($_POST["id2"])) {
        echo json_encode(["error" => "Les paramètres id1 et id2 sont requis"]);
        exit();
    }

    $id1 = $_POST["id1"];
    $id2 = $_POST["id2"];
    $query = "SELECT * FROM interaction WHERE (id = '$id1' AND id_1 = '$id2') OR (id = '$id2' AND id_1 = '$id1');";

    $response = array();

    $conn->query("SET NAMES utf8");
    $result = $conn->query($query);
    
    while ($row = $result->fetch()) {
        $response[] = $row;
    }

    $result->closeCursor();
    header('Content-Type: application/json');
    echo json_encode($response, JSON_PRETTY_PRINT);

}