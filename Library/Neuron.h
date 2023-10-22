#pragma once
#include <cmath>

class Neuron
{
public:
	double value, error, ActiveValue, bias;

	void Activate(){
		ActiveValue = 1.0 / (1.0 + exp(-value)); 
	}
	double proiz(){
		return ActiveValue * (1.0 - ActiveValue); 
	}

};

