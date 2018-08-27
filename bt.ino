void setup() {
  // put your setup code here, to run once:
  SerialUSB.begin(9600);
  Serial5.begin(9600);
  pinMode(3, INPUT_PULLUP);
  SerialUSB.println("aaaaa");
}

void loop() {
  SerialUSB.println(digitalRead(3));

  if(!digitalRead(3)){
  Serial5.write("1");
  delay(500);
  }
}
