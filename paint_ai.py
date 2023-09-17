import pygame
import math
import pathlib
from pathlib import Path
import cv2
import ctypes
import pygame as pg
from ctypes import *
ctypes.windll.shell32.SetCurrentProcessExplicitAppUserModelID(u"my.app.id.01")
pygame.init()

icon = pg.image.load(Path("files4paint", "icon.png"))

window = pygame.display.set_mode((900, 750))
pygame.display.set_caption("Create test's for learning AI")
pygame.display.set_icon(icon)

pg.display.set_icon(icon)
screen = pg.Surface((900, 750))
clock = pygame.time.Clock()


WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GRAY = (105, 105, 105)
GRAY2 = (62, 68, 82)
GRAYD = (58, 58, 58)
GRAYL = (180, 180, 180)
RED = (210, 30, 30)
DARKBLUE = (35, 39, 46)
DARKBLUE2 = (41, 46, 58)
GRAYBLUE = (105, 110, 118)
GRAYBLUE2 = (156, 166, 181)
TEXTGRAY = (146, 156, 168)


rect3 = pygame.Rect((120, 50, 640, 640))
rect7 = pygame.Rect((800, 50, 50, 641))
rect8 = pygame.Rect((120, 50, 640, 640))
rect11 = pygame.Rect((820, 0, 80, 25))
rect1 = pygame.Rect((30, 700, 730, 40))
rect2 = pygame.Rect((377, 722, 80, 17))
rect4 = pygame.Rect((400, 702, 70, 17))


pygame.font.init()
font1 = pygame.font.SysFont("Bahnschrift", 65)
font2 = pygame.font.SysFont("Arial", 40)
font3 = pygame.font.SysFont("Verdana", 10)
font4 = pygame.font.SysFont("Verdana", 10)
font5 = pygame.font.SysFont("Verdana", 18)

resh = font5.render(str(windll.user32.GetSystemMetrics(1)), False, (TEXTGRAY))
resw = font5.render(
    str(windll.user32.GetSystemMetrics(0))+"x", False, (TEXTGRAY))
versiont = font4.render("Stable ver. 1.8.5", False, (TEXTGRAY))


savepic1 = pygame.image.load(Path("files4paint", "save1.png"))
savepic2 = pygame.image.load(Path("files4paint", "save2.png"))

clearpic1 = pygame.image.load(Path("files4paint", "clear1.png"))
clearpic2 = pygame.image.load(Path("files4paint", "clear2.png"))

penpic1 = pygame.image.load(Path("files4paint", "pen1.png"))
penpic2 = pygame.image.load(Path("files4paint", "pen2.png"))

erasepic1 = pygame.image.load(Path("files4paint", "erase1.png"))
erasepic2 = pygame.image.load(Path("files4paint", "erase2.png"))

pickpic1 = pygame.image.load(Path("files4paint", "pick1.png"))
pickpic2 = pygame.image.load(Path("files4paint", "pick2.png"))


thicknesspic1l = pygame.image.load(Path("files4paint", "thickness1l.png"))
thicknesspic2l = pygame.image.load(Path("files4paint", "thickness2l.png"))
thicknesspic3l = pygame.image.load(Path("files4paint", "thickness3l.png"))
thicknesspic1d = pygame.image.load(Path("files4paint", "thickness1d.png"))
thicknesspic2d = pygame.image.load(Path("files4paint", "thickness2d.png"))
thicknesspic3d = pygame.image.load(Path("files4paint", "thickness3d.png"))

letterspic = pygame.image.load(Path("files4paint", "ls.png"))
cleanaftersavepic = pygame.image.load(
    Path("files4paint", "cleanaftersave.png"))
showgridpic = pygame.image.load(Path("files4paint", "showgrid.png"))
showfpspic = pygame.image.load(Path("files4paint", "showfps.png"))
showlogpic = pygame.image.load(Path("files4paint", "showlog.png"))
logpic = pygame.image.load(Path("files4paint", "log.png"))


video = cv2.VideoCapture("files4paint\scrsar2.avi")
success, video_image = video.read()
fps = video.get(cv2.CAP_PROP_FPS)


app = True
matrix = [[0 for j in range(66)] for i in range(66)]
letterset = [
    "a",
    "b",
    "c",
    "d",
    "e",
    "f",
    "g",
    "h",
    "i",
    "j",
    "k",
    "l",
    "m",
    "n",
    "o",
    "p",
    "q",
    "r",
    "s",
    "t",
    "u",
    "v",
    "w",
    "x",
    "y",
    "z",
]
penset = True
thikpos = 1
flag = False
flag2 = False
flag_ctrls = False
screensaver = True
letternum = 1
fn = str()
xls = 0
yls = 0
xls2 = 0
yls2 = 0
vectordraw = ""
showfps = False
showgrid = False
cleanaftersave = False
showlog = False
FPS_MAX = 0
filesall = 0
filesletter = 0
fpst = font5.render(str(FPS_MAX), False, (TEXTGRAY))
while screensaver:
    clock.tick(fps)
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            screensaver = False

    success, video_image = video.read()
    if success:
        video_surf = pygame.image.frombuffer(
            video_image.tobytes(), video_image.shape[1::-1], "BGR"
        )
    else:
        screensaver = False
    window.blit(video_surf, (0, 0))
    pygame.display.flip()


def drawGrid():
    blockSize = 10
    for x in range(120, 760, blockSize):
        for y in range(50, 690, blockSize):
            rect = pygame.Rect(x, y, blockSize, blockSize)
            pygame.draw.rect(window, GRAYL, rect, 1)


def drawgui():
    window.fill(DARKBLUE)
    pygame.draw.rect(window, WHITE, rect8, 2000)
    pygame.draw.rect(window, GRAY, rect3, 1)
    window.blit(savepic1, (30, 50))
    window.blit(clearpic1, (30, 110))
    window.blit(penpic1, (30, 190))
    window.blit(erasepic1, (30, 250))
    window.blit(thicknesspic1d, (30, 310))
    window.blit(pickpic1, (30, 490))
    window.blit(pickpic1, (30, 525))
    window.blit(pickpic1, (30, 560))
    window.blit(pickpic1, (30, 595))
    window.blit(cleanaftersavepic, (60, 490))
    window.blit(showgridpic, (60, 525))
    window.blit(showfpspic, (60, 560))
    window.blit(showlogpic, (60, 595))
    window.blit(versiont, (810, 735))


def showloggui():
    pygame.draw.rect(window, DARKBLUE, rect2, 10000)
    pygame.draw.rect(window, DARKBLUE, rect4, 10000)
    window.blit(logpic, (30, 700))
    window.blit(resw, (170, 718))
    window.blit(resh, (224, 718))
    window.blit(fpst, (145, 699))
    folder = Path("letterspictures")
    folderl = Path("letterspictures", letterset[letternum - 1])
    filestotal = font5.render(
        str(len(list(folder.rglob("*")))-26), False, (TEXTGRAY))
    filesletter = font5.render(
        letterset[letternum - 1]+": "+str(len(list(folderl.rglob("*")))), False, (TEXTGRAY))
    window.blit(filestotal, (400, 699))
    window.blit(filesletter, (380, 717))


def drawletgui():
    window.blit(letterspic, (800, 50))
    rect6 = pygame.Rect((800, (letternum * 24.615)+25.5, 50, 25))
    pygame.draw.rect(window, GRAYBLUE, rect6, 2)


def clear():
    pygame.draw.rect(window, WHITE, rect8, 2000)
    for i in range(66):
        for j in range(66):
            matrix[i][j] = 0
    if showgrid:
        drawGrid()


def save():

    folder = Path("letterspictures", letterset[letternum - 1])
    if folder.is_dir():
        folder_c = len([1 for file in folder.iterdir()])
    filename = Path(
        "letterspictures",
        letterset[letternum - 1],
        letterset[letternum - 1] + str(folder_c + 1) + ".txt",
    )
    f = open(filename, "w")
    for i in range(64):
        for j in range(64):
            f.write(str(matrix[i][j]))
            f.write(" ")
        f.write("\n")
    f.close()
    if (cleanaftersave):
        clear()


def cleansquare(x, y, t):
    if (t == 1):
        rect = pygame.Rect((x * 10), (y * 10), 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)] = 0

    if (t == 2 and (x*10)-10 < 740 and (y*10)-10 < 670):
        rect = pygame.Rect((x * 10), (y * 10), 20, 20)
        pygame.draw.rect(window, WHITE, rect, 10)
        matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)] = 0
        matrix[round((pos[1] - 50) / 10)+1][round((pos[0] - 120) / 10)] = 0
        matrix[round((pos[1] - 50) / 10)+1][round((pos[0] - 120) / 10)+1] = 0
        matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)+1] = 0

    if (t == 3 and (x*10)-10 < 730 and (y*10)-10 < 660):
        rect = pygame.Rect((x * 10)+10, (y * 10)+10, 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        rect = pygame.Rect((x * 10)+10, (y * 10), 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        rect = pygame.Rect((x * 10)+10, (y * 10)+20, 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        rect = pygame.Rect((x * 10), (y * 10)+10, 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        rect = pygame.Rect((x * 10)+20, (y * 10)+10, 10, 10)
        pygame.draw.rect(window, WHITE, rect, 5)
        matrix[round((pos[1] - 50) / 10)+1][round((pos[0] - 120) / 10)+1] = 0
        matrix[round((pos[1] - 50) / 10)+1][round((pos[0] - 120) / 10)] = 0
        matrix[round((pos[1] - 50) / 10)+1][round((pos[0] - 120) / 10)+2] = 0
        matrix[round((pos[1] - 50) / 10)][round((pos[0] - 120) / 10)+1] = 0
        matrix[round((pos[1] - 50) / 10)+2][round((pos[0] - 120) / 10)+1] = 0


def drawsquare(x, y, t):
    global xls
    global yls
    global xls2
    global yls2
    global vectordraw
    xr = round((x-120) / 10)
    yr = round((y-50) / 10)
    if (t == 1):
        matrix[yr][xr] = 2
        rect = pygame.Rect(
            (round((pos[0]) / 10) * 10), (round((pos[1]) / 10) * 10), 10, 10)
        pygame.draw.rect(window, BLACK, rect, 5)
    if (t == 2 and x < 740 and y < 670):
        matrix[yr][xr] = 2
        matrix[yr+1][xr+1] = 2
        matrix[yr+1][xr] = 2
        matrix[yr][xr+1] = 2
        rect = pygame.Rect(
            (round((pos[0]) / 10) * 10), (round((pos[1]) / 10) * 10), 20, 20)
        pygame.draw.rect(window, BLACK, rect, 10)
    if (t == 3 and x < 730 and y < 660):
        if (xls2 != xls):
            xls2 = xls
        if (yls2 != yls):
            yls2 = yls
        xls = round((pos[0] - 50) / 10)
        yls = round((pos[1] - 120) / 10)
        rectd1 = pygame.Rect(round((xls)*10)+50, round(yls*10)+130, 10, 10)
        rectd2 = pygame.Rect(round((xls)*10)+70, round(yls*10)+130, 10, 10)
        rectd3 = pygame.Rect(round((xls)*10)+60, round(yls*10)+140, 10, 10)
        rectd4 = pygame.Rect(round((xls)*10)+60, round(yls*10)+120, 10, 10)
        if (xls != xls2):
            if (xls-xls2 == 1):
                vectordraw = "right"
                rec = pygame.Rect(round((xls)*10)+50,
                                  round(yls*10)+130, 10, 10)
                pygame.draw.rect(window, BLACK, rec, 5)
                pygame.draw.rect(window, GRAYD, rectd2, 5)
                pygame.draw.rect(window, GRAYD, rectd3, 5)
                pygame.draw.rect(window, GRAYD, rectd4, 5)
                matrix[yls+7][xls-6] = 1
                matrix[yls+9][xls-6] = 1
                matrix[yls+8][xls-5] = 1
                matrix[yls+8][xls-6] = 2
            else:
                vectordraw = "left"
                rec = pygame.Rect(round((xls)*10)+70,
                                  round(yls*10)+130, 10, 10)
                pygame.draw.rect(window, BLACK, rec, 5)
                pygame.draw.rect(window, GRAYD, rectd1, 5)
                pygame.draw.rect(window, GRAYD, rectd3, 5)
                pygame.draw.rect(window, GRAYD, rectd4, 5)
                matrix[yls+7][xls-6] = 1
                matrix[yls+9][xls-6] = 1
                matrix[yls+8][xls-7] = 1
                matrix[yls+8][xls-6] = 2
        if (yls != yls2):
            if (yls-yls2 == 1):
                vectordraw = "down"
                rec = pygame.Rect(round((xls)*10)+60,
                                  round(yls*10)+120, 10, 10)
                pygame.draw.rect(window, BLACK, rec, 5)
                pygame.draw.rect(window, GRAYD, rectd1, 5)
                pygame.draw.rect(window, GRAYD, rectd2, 5)
                pygame.draw.rect(window, GRAYD, rectd3, 5)
                matrix[yls+9][xls-6] = 1
                matrix[yls+8][xls-7] = 1
                matrix[yls+8][xls-5] = 1
                matrix[yls+8][xls-6] = 2
            else:
                vectordraw = "up"
                rec = pygame.Rect(round((xls)*10)+60,
                                  round(yls*10)+140, 10, 10)
                pygame.draw.rect(window, BLACK, rec, 5)
                pygame.draw.rect(window, GRAYD, rectd1, 5)
                pygame.draw.rect(window, GRAYD, rectd2, 5)
                pygame.draw.rect(window, GRAYD, rectd4, 5)
                matrix[yls+7][xls-6] = 1
                matrix[yls+8][xls-7] = 1
                matrix[yls+8][xls-5] = 1
                matrix[yls+8][xls-6] = 2
        rect = pygame.Rect((round((x) / 10) * 10)+10,
                           (round((y) / 10) * 10)+10, 10, 10)
        pygame.draw.rect(window, BLACK, rect, 5)


drawgui()
drawletgui()

while app:
    if showlog:
        showloggui()

    if showfps:
        pygame.draw.rect(window, DARKBLUE, rect11, 50)
        fps_c = font3.render(
            "FPS: " + str(round(clock.get_fps())), False, (RED))
        window.blit(fps_c, (840, 3))

    clock.tick(FPS_MAX)
    pressed = pygame.mouse.get_pressed()
    pos = pygame.mouse.get_pos()
    ctrls_pressed = (event.type == pygame.KEYDOWN) and (
        event.key == pygame.K_s) and (event.mod & pygame.KMOD_CTRL)
    print(pos)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            app = False

    if (pos[0] > 120 and pos[0] < 755) and (pos[1] > 50 and pos[1] < 685):
        if pressed[0] and penset:
            drawsquare(pos[0], pos[1], thikpos)
        if pressed[0] and not penset:
            cleansquare(round((pos[0]) / 10), round((pos[1]) / 10), thikpos)

    if (
        pressed[0]
        and flag2 == False
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 50 and pos[1] < 100)
    ):
        save()
        flag2 = True
    if pressed[0] == False:
        flag2 = False

    if (ctrls_pressed and not flag_ctrls):
        save()
        flag_ctrls = True
    if (not ctrls_pressed):
        flag_ctrls = False

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 110 and pos[1] < 160)
    ):
        clear()

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 190 and pos[1] < 240)
    ):
        penset = True

    if (
        (pressed[0])
        and (pos[0] > 30 and pos[0] < 80)
        and (pos[1] > 250 and pos[1] < 300)
    ):
        penset = False

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 50 and pos[1] < 100):
        window.blit(savepic2, (30, 50))
    else:
        window.blit(savepic1, (30, 50))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 110 and pos[1] < 160):
        window.blit(clearpic2, (30, 110))
    else:
        window.blit(clearpic1, (30, 110))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 190 and pos[1] < 240) or (penset):
        window.blit(penpic2, (30, 190))
    else:
        window.blit(penpic1, (30, 190))

    if (pos[0] > 30 and pos[0] < 80) and (pos[1] > 250 and pos[1] < 300) or (not penset):
        window.blit(erasepic2, (30, 250))
    else:
        window.blit(erasepic1, (30, 250))

    if (pos[0] > 40 and pos[0] < 58) and (pos[1] > 327 and pos[1] < 442) and pressed[0]:
        thikpos = round((pos[1]-308)/38.333)

    if (pressed[0] == 0):
        flag = False

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 560 and pos[1] < 585) and pressed[0] and flag == 0 and showfps == 0:
        showfps = True
        flag = True
        window.blit(pickpic2, (30, 560))

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 560 and pos[1] < 585) and pressed[0] and not flag and showfps:
        showfps = False
        window.blit(pickpic1, (30, 560))
        flag = True
        pygame.draw.rect(window, DARKBLUE, rect11, 50)

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 490 and pos[1] < 515) and pressed[0] and flag == 0 and cleanaftersave == 0:
        cleanaftersave = True
        flag = True
        window.blit(pickpic2, (30, 490))

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 490 and pos[1] < 515) and pressed[0] and not flag and cleanaftersave:
        cleanaftersave = False
        window.blit(pickpic1, (30, 490))
        flag = True

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 525 and pos[1] < 550) and pressed[0] and flag == 0 and showgrid == 0:
        showgrid = True
        flag = True
        window.blit(pickpic2, (30, 525))
        clear()

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 525 and pos[1] < 550) and pressed[0] and not flag and showgrid:
        showgrid = False
        window.blit(pickpic1, (30, 525))
        flag = True
        clear()
# >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 595 and pos[1] < 620) and pressed[0] and flag == 0 and showlog == 0:
        showlog = True
        flag = True
        window.blit(pickpic2, (30, 595))

    if (pos[0] > 30 and pos[0] < 55) and (pos[1] > 595 and pos[1] < 620) and pressed[0] and not flag and showlog:
        showlog = False
        window.blit(pickpic1, (30, 595))
        flag = True
        pygame.draw.rect(window, DARKBLUE, rect1, 10000)


# >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    if (pos[0] > 40 and pos[0] < 58) and (pos[1] > 327 and pos[1] < 442):

        if (thikpos == 1):
            window.blit(thicknesspic1l, (30, 310))
        if (thikpos == 2):
            window.blit(thicknesspic2l, (30, 310))
        if (thikpos == 3):
            window.blit(thicknesspic3l, (30, 310))
    else:
        if (thikpos == 1):
            window.blit(thicknesspic1d, (30, 310))
        if (thikpos == 2):
            window.blit(thicknesspic2d, (30, 310))
        if (thikpos == 3):
            window.blit(thicknesspic3d, (30, 310))

    if (
        (pressed[0])
        and (pos[0] > 800 and pos[0] < 850)
        and (pos[1] > 50 and pos[1] < 690)
    ):
        letternum = math.floor(((pos[1] - 50) / 24.615) + 1)
        drawletgui()
        rect6 = pygame.Rect((800, (letternum * 24.615)+25.5, 50, 25))
        pygame.draw.rect(window, GRAY2, rect7, 1)
        pygame.draw.rect(window, GRAYBLUE, rect6, 2)
    pygame.draw.rect(window, GRAYD, rect3, 1)
    pygame.display.flip()
