using System;
using System.IO;
//
//
using CherryMPServer;

public class Animator : Script
{
    public Animator()
    {
        API.onClientEventTrigger += OnClientEvent;
    }

    public void OnClientEvent(Client player, string eventName, params object[] args)
    {
        if (eventName == "PlayAnimation")
        {
            string[] animInfo = args[0].ToString().Split(' ');
            string anim_group = animInfo[0];
            string anim_name = animInfo[1];
            player.setData("PLAYED_ANIMATION_GROUP", anim_group);
            player.setData("PLAYED_ANIMATION_NAME", anim_name);
            player.stopAnimation();
            player.playAnimation(anim_group, anim_name, 9);
        }
    }

    [Command("animator", "====================[~b~Аниматор~w~]====================\n" +
        "Запуск: ~g~/animator start \n" +
        "~w~Помощь: ~g~/animator help \n" +
        "~w~Остановить анимацию: ~g~/animator stop \n" +
        "~w~Сохранить анимацию: ~g~/animator save [savename] \n" +
        "~w~Пропустить анимацию: ~g~/animator skip [animation id]")]
    public void StartAnimator(Client player, string action = null, string action2 = "Anim")
    {
        if (!player.hasData("ANIMATOR_OPEN")) player.setData("ANIMATOR_OPEN", false);
        bool AnimatorOpen = player.getData("ANIMATOR_OPEN");

        if (action == null)
        {
            if (!AnimatorOpen)
            {
                player.setData("ANIMATOR_OPEN", true);
                player.triggerEvent("StartClientAnimator", animator.animations.AllAnimations);
                player.sendChatMessage("~b~[ANIMATOR]: ~w~Аниматор ~g~Включен~w~. Напишите ~g~/animator help ~w~для большей информации.");
            }
            else
            {
                player.resetData("ANIMATOR_OPEN");
                player.triggerEvent("StopClientAnimator");
                player.stopAnimation();
                player.sendChatMessage("~b~[ANIMATOR]: ~w~Аниматор ~r~Выключен~w~.");
            }
        }

        if (action != null && AnimatorOpen)
        {
            if (AnimatorOpen)
            {
                if (action == "save") SaveAnimatorData(player, action2);

                if (action == "skip") SkipAnimatorData(player, action2);

                if (action == "help")
                {
                    player.sendChatMessage("=================================[~b~Аниматор~w~]================================");
                    player.sendChatMessage("Используйте ~y~Влево ~w~и ~y~Вправо ~w~стрелки чтобы переключать анимации.");
                    player.sendChatMessage("Используйте ~y~Вверх ~w~и ~y~Вниз ~w~стрелки чтобы переключать анимации по 100.");
                    player.sendChatMessage("Вы также можете пропустить специфический ID, используйте ~y~/animator skip [number]~w~.");
                    player.sendChatMessage("Если вы желаете сохранить анимацию в .txt файл, используйте ~y~/animator save [savename]~w~.");
                    player.sendChatMessage("~w~Некоторые анимации не предназначены для модели человека!");
                }

                if (action == "stop")
                {
                    player.sendChatMessage("~b~[ANIMATOR]: ~w~Анимация остановлена.");
                    player.stopAnimation();
                }
            }
            else
            {
                player.sendChatMessage("~b~[ANIMATOR]: ~r~Вы должны сначала запустить аниматор! ~y~/animator.");
            }
        }
    }

    public void SaveAnimatorData(Client player, string name)
    {
        string anim_group = player.getData("PLAYED_ANIMATION_GROUP");
        string anim_name = player.getData("PLAYED_ANIMATION_NAME");
        File.AppendAllText("Saved_Animations.txt", string.Format("{0}:          {1} {2}", name, anim_group, anim_name) + Environment.NewLine);
        player.sendChatMessage(string.Format("~b~[ANIMATOR]: ~w~Анимация сохранена! Имя: ~g~{0} ~w~Anim: ~y~{1} ~b~{2}", name, anim_group, anim_name));
    }

    public void SkipAnimatorData(Client player, string animationID)
    {
        int ID;
        if (Int32.TryParse(animationID, out ID))
        {
            int animations_amount = animator.animations.AllAnimations.Count - 1;
            if (ID > animations_amount || ID < 0)
            {
                player.sendChatMessage("~b~[ANIMATOR]: ~w~ID должен быть между 0 и " + animations_amount + "!");
                return;
            }
            player.triggerEvent("SkipAnimatorData", ID);
        }
        else
        {
            player.sendChatMessage("~b~[ANIMATOR]: ~r~Неверное число!");
        }
    }
}