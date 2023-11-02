#include <Windows.h>
#include <string>

int main(){

    const char* NetworkHandlerPath = "..\\Network\\NetworkHandler.exe";
    std::string TestGeneratorPath = "..\\TrainingTests\\RandomTestGenerator.exe";


    system(NetworkHandlerPath);

    int n = 10000;//default number of tests for test generator
    system(TestGeneratorPath.c_str());
    system(std::to_string(n).c_str());

    // STARTUPINFO si;
    // PROCESS_INFORMATION pi;
    // ZeroMemory(&si, sizeof(si));
    // si.cb = sizeof(si);
    // ZeroMemory(&pi, sizeof(pi));

    // // Запускаем процесс
    // CreateProcess("installer.exe", NULL, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi);

    // // Ожидаем завершения процесса
    // WaitForSingleObject(pi.hProcess, INFINITE);

    // // Закрываем дескрипторы процесса и потока
    // CloseHandle(pi.hProcess);
    // CloseHandle(pi.hThread);
}