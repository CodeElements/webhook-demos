<?php
include 'include.php';

if ($_GET['secret'] != $secret) {
    exit('Invalid secret');
}

$jsonData = utf8_encode(file_get_contents("php://input"));
$licenseData = json_decode($jsonData, true);

$emailTemplate = file_get_contents("emailTemplate.html");
foreach ($licenseData as $key => $value) {
    $emailTemplate = str_replace('{{' . $key . '}}', $value, $emailTemplate);
}

$customerEmail = $licenseData['customerEmail'];
$customerName = $licenseData['customerName'];
$subject = 'Purchase Confirmation';

// To send HTML mail, the Content-type header must be set
$headers  = 'MIME-Version: 1.0' . "\r\n";
$headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";

$headers .= "To: $customerName <$customerEmail>\r\n";
$headers .= 'From: MyCompany <hello@yourwebsite.com>' . "\r\n";

// Mail it
mail($customerEmail, $subject, $emailTemplate, $headers);

//tell CodeElements that the webhook executed successfully
exit('*ok*');