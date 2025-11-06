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

$username = $_POST["name"];
$country = $_POST["country"];
$age = $_POST["age"];
$gender = $_POST["gender"];

$sql = "INSERT INTO users (Age, Gender, Country, Name)
VALUES ('$age', '$gender', '$country', '$username')";

if ($conn->query($sql) === TRUE)
{
	$last_id = $conn->insert_id;
	echo $last_id;
} else {
	echo "Error: " . $sql . "<br>" . $conn->error;
}

?>