#include <Windows.h>
#include <string>

int main(){

    const char* NetworkHandlerPath = "..\\Network\\NetworkHandler.exe";
    std::string TestGeneratorPath = "..\\TrainingTests\\RandomTestGenerator.exe";


    system(NetworkHandlerPath);

    int n = 10000;
    system(TestGeneratorPath.c_str());
    system(std::to_string(n).c_str());

}