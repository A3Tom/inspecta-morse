#include <string>

int led_red = 5;
int led_blue = 4;
int led_yellow = 27;
int led_green = 14;
int BUTTON_PIN = 16;
int dit_threshold = 400;
int space_threshold = 750;

long pressStart = 0;
long lastAction = 0;

int lastState = HIGH;
int currentState;

bool insertSpace = false;
bool insertNextChar = false;

String outputText = "";

void setup() {
  Serial.begin(9600);
  pinMode(led_red, OUTPUT);
  pinMode(led_blue, OUTPUT);
  pinMode(led_yellow, OUTPUT);
  pinMode(led_green, OUTPUT);
  pinMode(BUTTON_PIN, INPUT_PULLUP);
}

void loop() {
  currentState = digitalRead(BUTTON_PIN);

  if(currentState == HIGH)
  {
    if(lastState == LOW)
    {
      Serial.println("key pressed down");
      pressStart = millis();
      lastState = currentState;
    }
    
    if(millis() - pressStart > dit_threshold * 2.5)
    {
      digitalWrite(led_green, HIGH);
      digitalWrite(led_yellow, LOW);
      digitalWrite(led_blue, LOW);
      insertSpace = true;
    }
    else if (millis() - pressStart > dit_threshold)
    {
      digitalWrite(led_yellow, HIGH);
      digitalWrite(led_blue, LOW);
    }
    else
    {
      digitalWrite(led_blue, HIGH);
      digitalWrite(led_green, LOW);
    }

    lastAction = millis();
    digitalWrite(led_red, LOW);
  }
  else
  {
    if(lastState == HIGH)
    {
      long duration = millis() - pressStart;
      if (insertNextChar)
        outputText += "_";

      if(insertSpace)
        outputText += "/";
      else if(duration <= dit_threshold)
        outputText += ".";
      else
        outputText += "-";

      Serial.print("Press duration: ");
      Serial.print(duration);
      Serial.println("ms");
      pressStart = 0;
      lastState = currentState;
      insertSpace = false;
      insertNextChar = false;
    }
    digitalWrite(led_red, HIGH);
    digitalWrite(led_blue, LOW);
    digitalWrite(led_yellow, LOW);

    if(insertNextChar)
      digitalWrite(led_green, HIGH);
    else
      digitalWrite(led_green, LOW);
  }

  if (lastAction != 0 && millis() - lastAction > space_threshold)
  {
    insertNextChar = true;
  }
  if(lastAction != 0 && millis() - lastAction > space_threshold * 3)
  {
    insertNextChar = false;

    Serial.print("Morse Message : ");
    Serial.println(outputText);
    //char buffer[4096];
    //sprintf(buffer, "Morse Message : %s", outputText);
    //Serial.println(buffer);
    outputText = "";

    send_led_show();

    lastAction = 0;
  }
}


void send_led_show()
{
  int lightDelay = 75;
  
  all_leds_low();
  delay(lightDelay);

  digitalWrite(led_red, HIGH);
  delay(lightDelay);
  digitalWrite(led_blue, HIGH);
  delay(lightDelay);
  digitalWrite(led_yellow, HIGH);
  delay(lightDelay);
  digitalWrite(led_green, HIGH);
  delay(lightDelay);

  all_leds_low();
  delay(50);
}

void all_leds_low()
{
  digitalWrite(led_red, LOW);
  digitalWrite(led_blue, LOW);
  digitalWrite(led_yellow, LOW);
  digitalWrite(led_green, LOW);
}
