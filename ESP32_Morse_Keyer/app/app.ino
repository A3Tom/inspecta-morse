#include <string>

int led_red = 5;
int led_blue = 4;
int led_green = 14;
int BUTTON_PIN = 16;
int dit_threshold = 200;
int send_threshold = 2000;

long pressStart = 0;
long lastAction = 0;

int lastState = HIGH;
int currentState;

bool lastCharIsSpace = false;

String outputText = "";

void setup() {
  Serial.begin(9600);
  pinMode(led_red, OUTPUT);
  pinMode(led_blue, OUTPUT);
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

    lastAction = millis();
    digitalWrite(led_blue, HIGH);
    digitalWrite(led_red, LOW);
  }
  else
  {
    if(lastState == HIGH)
    {
      long duration = millis() - pressStart;
      if(duration <= dit_threshold)
        outputText += ".";
      else
        outputText += "-";

      Serial.print("Press duration: ");
      Serial.print(duration);
      Serial.println("ms");
      pressStart = 0;
      lastState = currentState;
    }
    digitalWrite(led_red, HIGH);
    digitalWrite(led_blue, LOW);
  }

  if(lastAction != 0 && !lastCharIsSpace && millis() - lastAction > dit_threshold * 2)
  {
    outputText += "/";
    
    digitalWrite(led_green, HIGH);
    delay(50);
    digitalWrite(led_green, LOW);
  }
  else if(lastAction != 0 && millis() - lastAction > send_threshold)
  {
    char buffer[100];
    sprintf(buffer, "Morse Message : %s", outputText);
    Serial.println(buffer);
    outputText = "";
    lastAction = 0;

    all_leds(HIGH);
    delay(350);
    all_leds(LOW);
  }
}

void all_leds(int state)
{
  digitalWrite(led_red, state);
  digitalWrite(led_blue, state);
  digitalWrite(led_green, state);
}
