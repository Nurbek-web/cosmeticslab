using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour {
    [Header("State")]
    public int currentDay = 1;
    public bool woreCoat = false;
    public bool minigameCompleted = false;
    public bool minigameCorrect = false;

    [Header("Refs")]
    public DoorController2D labDoor;
    public AnnaDialogueUI annaUI;
    public ReportUI reportUI;
    public BookUI bookUI;
    public MinigameController minigame;

    [Header("Data")]
    public CosmeticSample CurrentSample;
    public List<string> bannedList = new List<string>{
        "гидрохинон", "hydroquinone",
        "свинец", "lead",
        "кадмий", "cadmium",
        "ртуть", "mercury",
        "бисфенол", "bisphenol"
    };

    void Start() {
        SetupDay1();
    }

    void SetupDay1() {
        woreCoat = false;
        minigameCompleted = false;
        minigameCorrect = false;
        labDoor.SetLocked(true);

        // Образец дня:
        CurrentSample = new CosmeticSample {
            displayName = "Крем №A-01",
            declaredIngredients = new List<string> { "вода", "глицерин", "пантенол" },
            hasCertificate = true
        };

        annaUI.Show("День 1. Надень халат у шкафа, зайди в лабораторию и на столе проверь образец. Если сертификат есть и в составе нет запретов — одобри.");
    }

    public void OnWoreCoat() {
        woreCoat = true;
        labDoor.SetLocked(false);
        annaUI.Show("Дверь открыта. Иди к столу и проверь образец (E). Книга-справочник — клавиша B.");
    }

    public void OnMinigameFinished(bool correct) {
        minigameCompleted = true;
        minigameCorrect = correct;
        annaUI.Show("Проверка завершена. Вернись в прихожую и сними халат у шкафа, чтобы получить отчёт.");
    }

    public bool CanUseMinigame() {
        return woreCoat && !minigameCompleted;
    }

    public void OnTryUndress() {
        if (!minigameCompleted) {
            annaUI.Show("Сначала выполни проверку на столе.");
            return;
        }

        // Показать отчёт и предложить продолжение/повтор
        reportUI.Open(currentDay, minigameCorrect, () => {
            if (minigameCorrect) {
                currentDay = 2;
                annaUI.Show("Отлично! День 1 пройден. В следующий раз добавим биосенсор (гидрохинон).");
                // Пока остаёмся на той же сцене, можно заново позвать SetupDay1 или подготовить Day 2 на следующем шаге туториала.
            } else {
                annaUI.Show("Не всё верно. Попробуй заново День 1: надень халат и проверь образец на столе.");
                SetupDay1();
            }
        });
    }

    // Проверка решения для Дня 1:
    public bool CheckDecision_Day1(CosmeticSample sample, bool playerApproved) {
        bool labelHasBanned = ContainsBanned(sample.declaredIngredients);
        // Правильная логика Дня 1:
        //  - Если есть запрещённые в составе => Reject.
        //  - Иначе, если есть сертификат => Approve.
        //  - Иначе => Reject.
        bool correctShouldApprove = !labelHasBanned && sample.hasCertificate;

        return (playerApproved == correctShouldApprove);
    }

    bool ContainsBanned(List<string> ingredients) {
        foreach (var ing in ingredients) {
            string low = ing.ToLower().Trim();
            foreach (var banned in bannedList) {
                if (low.Contains(banned)) return true;
            }
        }
        return false;
    }
}
