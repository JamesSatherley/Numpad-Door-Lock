

#include <Keypad.h>

String serialData;
int solenoid = 2;
byte rowPins[4] = {13, 12, 11, 10}; 
byte colPins[4] = {7, 5, 6, 4};

char hexaKeys[4][4] = {
  {'1', '2', '3', 'A'},
  {'4', '5', '6', 'B'},
  {'7', '8', '9', 'C'},
  {'*', '0', '#', 'D'}
};

Keypad customKeypad = Keypad(makeKeymap(hexaKeys), rowPins, colPins, 4, 4); 

void setup() {
  pinMode(solenoid, OUTPUT);
  Serial.begin(9600);
  Serial.setTimeout(10);
  digitalWrite(solenoid, HIGH); 
}

void loop(){
  char customKey = customKeypad.getKey();
  Serial.println(customKey);
  delay(30);
}

void serialEvent() {
  serialData = Serial.readString();
  
  if(serialData == "CLOSE"){
  digitalWrite(solenoid, HIGH); 
  }else if (serialData == "OPEN"){
  digitalWrite(solenoid, LOW); 
  }
}
