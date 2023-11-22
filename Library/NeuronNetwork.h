#pragma once
#include "Neuron.h"
#include <iostream>
#include <iomanip>
#include <fstream>

class NeuronNetwork {
public:

    int layers;
    int* size;
    Neuron** neurons;
    double*** weights;

    NeuronNetwork(int n, int* p) {

        /* function that allocates memory locations (construct network)*/

        layers  = n; // set layers number
        size    = new int[n]; // set size as massive
        neurons = new Neuron * [n]; // set neurons layers
        weights = new double** [n - 1]; //  because wights have n-1 layers


        for (int i = 0; i < n; i++) {

            size[i] = p[i]; // set size array
            neurons[i] = new Neuron[p[i]+1]; // +1 for bias active value 

            neurons[i][p[i]].ActiveValue = 1.0; // Bias active value is only 1

            if(i == n-1) break; // Because bias has n-1 layers 
            weights[i] = new double* [p[i]+1];// +1 for bias weights
            for (int j = 0; j <= p[i]; j++) { 
                weights[i][j] = new double[p[i + 1]]; // i+1 because next layer
                for(int k = 0;k < p[i+1];k++){
                    weights[i][j][k] = (rand() % 100) * 0.01; // Set random value for weights
                }
            }

        }

    }
    ~NeuronNetwork() {
        /* function that free allocated memory */
        delete[] size, neurons, weights;
    }

    void SetInput(double* p) {
        for (int i = 0; i < size[0]; i++) {
            neurons[0][i].ActiveValue = p[i]; // set input layer
        }
    }

    void ForwardFeed() {

        /* hand input to output*/

        for (int i = 1; i < layers; i++) {
            for (int j = 0; j < size[i]; j++) {
                neurons[i][j].value = 0.0;
                for (int k = 0; k <= size[i - 1]; k++) { // <= for bias neuron
                    neurons[i][j].value += neurons[i - 1][k].ActiveValue * weights[i - 1][k][j]; 
                }
                neurons[i][j].Activate(); // use activation function
            }
        }
            
    }
    int Predict() {

        double MaximalValue =  neurons[layers - 1][0].ActiveValue;
        int MaxIndex = 0;

        /* find  maximal value in output layer for prediction*/

        for (int i = 1; i < size[layers - 1]; i++) {
            if (MaximalValue < neurons[layers - 1][i].ActiveValue) {
                MaximalValue = neurons[layers - 1][i].ActiveValue;
                MaxIndex = i;
            }
        }

        return MaxIndex; // returning index of neuron that fits best
    }

    double ErrorCouter(double* ra) {
        
        /* count error in output layer for assess of correctness*/

        double sum = 0.0;

        for (int i = 0; i < size[layers - 1]; i++) {
            sum += pow((neurons[layers - 1][i].ActiveValue - ra[i]), 2);
        }

        return sum; // returning the error sum in output error
    }

    void BackPropagation(double* ra, double ls) {

        /* the backward function for counting of error in all layers and for changing weights to more correct */

        for (int i = 0; i < size[layers - 1]; i++) {
            neurons[layers - 1][i].error = 2 * (neurons[layers - 1][i].ActiveValue - ra[i]);
        }

        for (int i = layers - 2; i >= 0; i--) {
            for (int j = 0; j < size[i]+1; j++) {
                neurons[i][j].error = 0.0;
                for (int k = 0; k < size[i + 1]; k++) {
                    neurons[i][j].error   += (neurons[i + 1][k].error) * (neurons[i + 1][k].DerivationVal()) * (weights[i][j][k]); // calculate value
                    weights[i][j][k] -= ls * (neurons[i + 1][k].error) * (neurons[i + 1][k].DerivationVal()) * (neurons[i][j].ActiveValue); // correct weights
                }
            }
        }
    }

    void SaveNetwork(const char *filename) {

        /* function that save network into file on disk*/

        std::ofstream fout(filename);
        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    fout << std::fixed << std::setprecision(8) << weights[i][j][k] << ' '; // saving weights
                }
                fout << '\n';
            }
        }
        fout << '\n';
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i+1];j++){
                fout << weights[i][size[i]][j] << ' '; // saving bias
            }
        }
        fout.close();
    }
    void LoadNetwork(const char *filename) {


        /* function that load network from file in disk*/

        std::ifstream fin(filename);
        for (int i = 0; i < layers - 1; i++) {
            for (int j = 0; j < size[i]; j++) {
                for (int k = 0; k < size[i + 1]; k++) {
                    fin >> weights[i][j][k]; // load wights
                }
            }
        }
        for(int i = 0;i < layers-1;i++){
            for(int j = 0;j < size[i+1];j++){
                fin >> weights[i][size[i]][j]; // load bias
            }
        }
        fin.close();
    }

    double* SoftMax(){

        /* function that calculate the probability of disease */

        double SE = 0;
        double* ans = new double[size[layers-1]];

        for(int i = 0;i < size[layers-1];i++){
            ans[i] = exp(neurons[layers-1][i].value); 
            SE += ans[i]; // calculating sum for function
        }

        for(int i = 0;i < size[layers-1];i++){
            ans[i] = ans[i]/SE*100.0; // calculating standart softmax function
        }

        return ans; // returnning array of probability (size of array = size of output layer)

    }

};
