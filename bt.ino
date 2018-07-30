void setup() {
  // put your setup code here, to run once:
  SerialUSB.begin(9600);
  Serial5.begin(9600);

  SerialUSB.println("aaaaa");
}

void loop() {
  // put your main code here, to run repeatedly:
  if (SerialUSB.available()) {
    Serial5.write(SerialUSB.read());
  }
  if (Serial5.available()) {
    SerialUSB.write(Serial5.read());
  }
 //SerialUSB.println(analogRead(A0)) ;
}
