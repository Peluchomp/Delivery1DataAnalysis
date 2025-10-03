<?php
$servername = "localhost";
$username = "paums3";
$password = "mbmQcfjTKPmm";

//Create connection
$conn = new mysqli($servername, $username, $password, "paums3");

//Check connection

if ($conn->connect_error) {

die("Connection failed: " . $conn->connect_error);
}
echo "Connected succcessfully";

$sql = "INSERT INTO Users (firstname, lastname, email)
VALUES ('John', 'Doe', 'john@example.com')";

if ($conn->query($sql) === TRUE) {
	echo "New record created successfully";
} else {
	echo " Error: " . $sql . "<br>" . $conn->error;
}
?>