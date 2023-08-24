import cv2
import os.path
import os

directory = 'assets\\images' # папка с картинками
files = os.listdir(directory) # массив с файлами
files_size = len(files) # кол-во картинок с буквами


f = open('assets\\lib.txt', 'w') # открываем файл для записи

for k in range(26): # проходимся по каждой букве

    l = 0
    
    path = directory+'\\letter'+str(chr(ord('a') + k)) # путь к букве
    while(os.path.isfile(path) == True):

        path2 = path + str(l) + '.png'
        l = l + 1
        
        img = cv2.imread(path, cv2.IMREAD_GRAYSCALE) # записываем картинку в переменную
        w,h = 64# ширина высота

        for i in range(w):
            for j in range(h):
                a = img[i, j] # параметр пикселя
                ans = round((255 - a) / 255, 1) # 1 - полностью черный, 0 - полностью белый
                f.write(str(ans)) # записываем параметр пикселя в файл
                f.write(' ')
            f.write('\n')  # разделители
        f.write(str(chr(ord('a') + k)))
f.close() # закрыть файл


