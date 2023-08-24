import cv2
import os.path
import os

directory = 'assets\\images' # папка с картинками
files = os.listdir(directory) # массив с файлами
files_size = len(files) # кол-во картинок с буквами


f = open('assets\\lib.txt', 'w') # открываем файл для записи

for k in range(files_size): # проходимся по каждой букве

    path = 'assets\\images\\'+'letter'+str(chr(ord('a') + k))+'.png' # путь к букве
    if (os.path.isfile(path) == True):

        img = cv2.imread(path, cv2.IMREAD_GRAYSCALE) # записываем картинку в переменную
        w = img.shape[0] # ширина
        h = img.shape[1] # высота

        for i in range(w):
            for j in range(h):
                a = img[i, j] # параметр пикселя
                ans = round((255 - a) / 255, 1) # 1 - полностью черный, 0 - полностью белый
                f.write(str(ans)) # записываем параметр пикселя в файл
                f.write(' ')
            f.write('\n')  # разделители
        f.write(str(chr(ord('a') + k)))
f.close() # закрыть файл


