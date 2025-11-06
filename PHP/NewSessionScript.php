<?php
$servername = "localhost";
$username = "paums3";
$password = "mbmQcfjTKPmm";

// Create Connection
$conn = new mysqli($servername, $username, $password, "paums3");

// Check Connection
if ($conn->connect_error)
{
	die("Connection failed:" . $conn->connect_error);
}

$userID = $_POST["player_id"];
$time = $_POST["time"];


$sql = "INSERT INTO Sessions (UserID, StartTime)
VALUES ('$userID', '$time')";

if ($conn->query($sql) === TRUE)
{
	$last_id = $conn->insert_id;
	echo (int)$last_id;
} else {
	echo "Error: " . $sql . "<br>" . $conn->error;
}
?>