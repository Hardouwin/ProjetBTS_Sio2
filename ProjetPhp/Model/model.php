<?php
function insertUser($nom ,$prenom, $mail)
{
    $url = 'http://127.0.0.1/ProjetPhp/Api/api_activite.php';
    $data = array('nom' => $nom, 'prenom' => $prenom ,'mail' => $mail);

    $options = array(
        'http' => array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'POST',
            'content' => http_build_query($data)

        )
        );
        $context = stream_context_create($options);
        $result = file_get_contents($url, false, $context);

        return $result;

}


function insertAct($nom ,$prenom, $mail ,$date ,$idA)
{
    $url = 'http://127.0.0.1/ProjetPhp/Api/api_activite.php';
    $data = array('nom' => $nom, 'prenom' => $prenom , 'mail' => $mail, 'date' => $date, 'idA' => $idA);

    $options = array(
        'http' => array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'POST',
            'content' => http_build_query($data)

        )
        );
        $context = stream_context_create($options);
        $result = file_get_contents($url, false, $context);

        return $result;

}



function selectUsers()
{
    $url = "http://127.0.0.1/ProjetPhp/Api/api.php";
    $options= array(
        'http'=> array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'GET'));

$context = stream_context_create($options);
$user = file_get_contents($url, false, $context);
return $user;

}

function selectAct()
{
    $url = "http://127.0.0.1/ProjetPhp/Api/api_activite.php";
    $options= array(
        'http'=> array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'GET'));

$context = stream_context_create($options);
$user = file_get_contents($url, false, $context);
return $user;

}

function selectEffet($id1 ,$id2)
{
    $url = 'http://127.0.0.1/ProjetPhp/Api/api.php';
    $data = array('id1' => $id1, 'id2' => $id2);

    $options = array(
        'http' => array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'POST',
            'content' => http_build_query($data)
        )
        );
        $context = stream_context_create($options);
        $result = file_get_contents($url, false, $context);

        return $result;

}


function selectCher()
{
    $url = "http://127.0.0.1/ProjetPhp/Api/api_chercheurs.php";
    $options= array(
        'http'=> array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'GET'));

$context = stream_context_create($options);
$user = file_get_contents($url, false, $context);
return $user;

}

function insertCher($nom ,$prenom, $mail ,$date ,$idA)
{
    $url = 'http://127.0.0.1/ProjetPhp/Api/api_chercheurs.php';
    $data = array('nom' => $nom, 'prenom' => $prenom , 'mail' => $mail, 'date' => $date, 'idA' => $idA);

    $options = array(
        'http' => array(
            'header' => "Content-type: application/x-www-form-urlencoded\r\n",
            'method' => 'POST',
            'content' => http_build_query($data)

        )
        );
        $context = stream_context_create($options);
        $result = file_get_contents($url, false, $context);

        return $result;

}
?>