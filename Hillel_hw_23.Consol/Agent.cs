using System;

namespace Hillel_hw_23.Consol
{
    public class Agent
    {
        public static readonly string ChooseMainAction = "Выберите действие: Добавить запись - 1, Обновить запись - 2, Поиск записи(ей) - 3 , " +
            "Удалить запись - 4, Отмена - 0.";

        public static async Task ConsoleMainInterface()
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Agent ======");
                Console.WriteLine(ChooseMainAction);
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Add record.
                        case 1:
                            //Здесь и далее в качестве токена будет заглушка, т.к. пока хз как адекватно из консоли отправить запрос на отмену.
                            await ConsoleAgentAddNew_Steps(CancellationToken.None);
                            break;

                        //Update record.
                        case 2:
                            await ConsoleUpdateInterface(CancellationToken.None);
                            break;

                        //Search record.
                        case 3:
                            await ConsoleSearchInterface(CancellationToken.None);
                            break;

                        //Delete record.
                        case 4:
                            await ConsoleDeleteInterface(CancellationToken.None);
                            break;

                        //Return from Agent table.
                        case 0:
                            return;

                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        public static async Task ConsoleAgentAddNew_Steps(CancellationToken token)
        {
            Console.WriteLine("Введите имя (обязательное поле <= 50 символов).");
            string fName = Console.ReadLine();
            Console.WriteLine("Введите фамилию (обязательное поле <= 50 символов).");
            string lName = Console.ReadLine();
            Console.WriteLine("Введите отчество (не обязательное поле <= 50 символов).");
            string mName = Console.ReadLine();
            Console.WriteLine("Введите ID отдела (обязательное поле).");
            int depId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID должности (обязательное поле).");
            int posId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID звания (обязательное поле).");
            int rankId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID текущего статуса (обязательное поле).");
            int statusId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите телефон (не обязательное поле <= 20 символов).");
            string phone = Console.ReadLine();
            Console.WriteLine("Введите адрес (не обязательное поле <= 100 символов).");
            string address = Console.ReadLine();

            await Core.Agent.AddNew(fName, lName, mName, depId, posId, rankId, statusId, phone, address, token);

            Console.WriteLine("Запись добавлена.");
        }

        private static async Task ConsoleSearchInterface(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Agent ====== Поиск ======");
                Console.WriteLine("Выберите параметр поиска: First name - 1, Last name - 2, Middle name - 3 (не доступно), Department ID - 4, " +
                    "Position ID - 5, Rank ID - 6 (не доступно), Status ID - 7, Phone - 8 (не доступно), Address - 9 (не доступно), Отмена - 0.");

                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                if (idAction == 0)
                {
                    return;
                }
                try
                {
                    AgentsToConsole(await SearchByField_Steps(idAction, token));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static async Task<List<Core.Agent>> SearchByField_Steps(int searchCase, CancellationToken token)
        {
            List<Core.Agent> agents = new();
            switch (searchCase)
            {
                //First name.
                case 1:
                    Console.WriteLine("Введите имя агента.");
                    agents = await Core.Agent.Search_ByFirstName(Console.ReadLine(), token);
                    break;
                //Last name.
                case 2:
                    Console.WriteLine("Введите фамилию агента.");
                    agents = await Core.Agent.Search_ByLastName(Console.ReadLine(), token);
                    break;
                //Middle name.
                //case 3:
                //    break;

                //Department ID.
                case 4:
                    Console.WriteLine("Введите ID департамента агента.");
                    agents = await Core.Agent.Search_ByDepartmentId(Int32.Parse(Console.ReadLine()), token);
                    break;

                //Position ID.
                case 5:
                    Console.WriteLine("Введите ID должности агента.");
                    agents = await Core.Agent.Search_ByPositionId(Int32.Parse(Console.ReadLine()), token);
                    break;

                //Rank ID.
                //case 6:
                //    break;

                //Status ID.
                case 7:
                    Console.WriteLine("Введите ID статуса агента.");
                    agents = await Core.Agent.Search_ByStatusId(Int32.Parse(Console.ReadLine()), token);
                    break;

                //Phone.
                //case 8:
                //    break;

                //Address.
                //case 9:
                //    break;

                default:
                    throw new Exception("Необходимо ввести ID из списка, указанного выше.");
            }
            return agents;
        }

        private static async Task ConsoleUpdateInterface(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Agent ====== Обновление записи ======");
                Console.WriteLine("Поиск записи - 1, Ввести ID записи - 2, Отмена - 0");
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Поиск записей в БД -> Выбор нужной из списка -> Обновление полей.
                        case 1:
                        SearchBeforeUpdate:
                            Console.WriteLine("====== Таблица Agent ====== Поиск ======");
                            Console.WriteLine("Выберите параметр поиска: First name - 1, Last name - 2, Middle name - 3 (не доступно), Department ID - 4, " +
                                "Position ID - 5, Rank ID - 6 (не доступно), Status ID - 7, Phone - 8 (не доступно), Address - 9 (не доступно), Отмена - 0.");

                            if (!int.TryParse(Console.ReadLine(), out int searchCase))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto case 1;
                            }
                            List<Core.Agent> agents = await SearchByField_Steps(searchCase, token);
                            AgentsToConsole(agents);
                        //Выбор действия после поиска в БД.
                        ActionsAfterSearch:
                            if (agents.Count() > 0)
                            {
                                Console.WriteLine("Повторить поиск - 1, Выбрать номер (не ID) - 2, Отмена - 0.");
                            }
                            else
                            {
                                Console.WriteLine("Повторить поиск - 1, Отмена - 0.");
                            }

                            if (!int.TryParse(Console.ReadLine(), out int selectedCase))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto ActionsAfterSearch;
                            }

                            switch (selectedCase)
                            {
                                //Возврат к выбору поля для поиска в БД.
                                case 1:
                                    goto SearchBeforeUpdate;
                                case 2:
                                    if (agents.Count > 0)
                                    {
                                        Console.WriteLine("Введите номер (не ID) записи из списка найденных. Отмена - 0.");
                                        if (!int.TryParse(Console.ReadLine(), out int selectedNumber))
                                        {
                                            Console.WriteLine(CustomMessages.NotAnId);
                                            goto ActionsAfterSearch;
                                        }
                                        if (selectedNumber == 0)
                                        {
                                            goto ActionsAfterSearch;
                                        }
                                        else if (selectedNumber > agents.Count)
                                        {
                                            Console.WriteLine("Указанный номер отсутствует в списке найденных.");
                                            goto case 2;
                                        }
                                        else
                                        {
                                            //Вот тут мы наконец-то определилсь с выбором объекта Core.Agent который далее будем редактировать.
                                            //-1 так выбираем номер, а не индекс.
                                            Core.Agent agent = agents[selectedNumber - 1];
                                            await UpdateRecord_Steps(agent, token);
                                        }
                                    }
                                    else
                                    {
                                        goto default;
                                    }
                                    break;
                                //Возврат к самому началу этапа обновения записи.
                                case 0:
                                    continue;
                                default:
                                    Console.WriteLine(CustomMessages.UnknownId);
                                    goto ActionsAfterSearch;
                            }
                            break;

                        //Выбор записи по ID -> Обновление полей.
                        case 2:
                            Console.WriteLine("Введите ID записи из БД. Отмена - 0.");
                            if (!int.TryParse(Console.ReadLine(), out int recordId))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto case 2;
                            }
                            //Возврат к самому началу этапа обновения записи.
                            if (recordId == 0)
                            {
                                continue;
                            }
                            else
                            {
                                var searchById = await Core.Agent.Search_ById(recordId, token);
                                if (searchById.Count == 0)
                                {
                                    Console.WriteLine($"Запись с id = {searchById} не найдена.");
                                    goto case 2;
                                }
                                else
                                {
                                    //Вот тут мы наконец-то определилсь с выбором объекта Core.Agent который далее будем редактировать.
                                    //Поиск по id вернёт либо 0 либо 1, но чтобы не делать под еденичный результат доп. методы, оставил так.
                                    Core.Agent agent = searchById[0];
                                    await UpdateRecord_Steps(agent, token);
                                }
                            }
                            break;

                        case 0:
                            return;

                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static async Task UpdateRecord_Steps(Core.Agent agent, CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine($"====== Таблица Agent ====== Обновление записи ID = {agent.ID} ======");
                Console.WriteLine(AgentToSring(agent));
                Console.WriteLine("Выберите параметр который небходимо отредактировать: First name - 1, Last name - 2, Middle name - 3, Department ID - 4, " +
                    "Position ID - 5, Rank ID - 6, Status ID - 7, Phone - 8, Address - 9. Отмена - 0. Применить изменения - 99.");

                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }

                try
                {
                    switch (idAction)
                    {
                        //First name.
                        case 1:
                            Console.WriteLine("Введите новое имя (обязательное поле <= 50 символов).");
                            agent.FirstName = Console.ReadLine();
                            break;

                        //Last name.
                        case 2:
                            Console.WriteLine("Введите новую фамилию (обязательное поле <= 50 символов).");
                            agent.LastName = Console.ReadLine();
                            break;

                        //Middle name.
                        case 3:
                            Console.WriteLine("Введите новое отчество (не обязательное поле <= 50 символов).");
                            string mName = Console.ReadLine();
                            break;

                        //Department ID.
                        case 4:
                            Console.WriteLine("Введите новый ID отдела (обязательное поле).");
                            agent.DepartmentID = Int32.Parse(Console.ReadLine());
                            break;

                        //Position ID.
                        case 5:
                            Console.WriteLine("Введите новый ID должности (обязательное поле).");
                            agent.PositionID = Int32.Parse(Console.ReadLine());
                            break;

                        //Rank ID.
                        case 6:
                            Console.WriteLine("Введите новый ID звания (обязательное поле).");
                            agent.RankID = Int32.Parse(Console.ReadLine());
                            break;

                        //Status ID.
                        case 7:
                            Console.WriteLine("Введите новый ID текущего статуса (обязательное поле).");
                            agent.StatusID = Int32.Parse(Console.ReadLine());
                            break;

                        //Phone.
                        case 8:
                            Console.WriteLine("Введите новый телефон (не обязательное поле <= 20 символов).");
                            agent.Phone = Console.ReadLine();
                            break;

                        //Address.
                        case 9:
                            Console.WriteLine("Введите новый адрес (не обязательное поле <= 100 символов).");
                            agent.Address = Console.ReadLine();
                            break;

                        case 0:
                            return;

                        case 99:
                            await Core.Agent.Update(agent, token);
                            Console.WriteLine(CustomMessages.RecordUpdateSuccess);
                            return;

                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }

            }

        }

        //До этапа окончательного выбора записи, удаление практически полностью совпадает с поиском записи,
        //т.к. я не особо представляю как можно удалить что-то, не зная что в БД вообще есть.
        private static async Task ConsoleDeleteInterface(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Agent ====== Удаление записи ======");
                Console.WriteLine("Поиск записи - 1, Ввести ID записи - 2, Отмена - 0");
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Поиск записей в БД -> Выбор нужной из списка -> Удаление.
                        case 1:
                        SearchBeforeUpdate:
                            Console.WriteLine("====== Таблица Agent ====== Удаление записи ======");
                            Console.WriteLine("Выберите параметр поиска: First name - 1, Last name - 2, Middle name - 3 (не доступно), Department ID - 4, " +
                                "Position ID - 5, Rank ID - 6 (не доступно), Status ID - 7, Phone - 8 (не доступно), Address - 9 (не доступно), Отмена - 0.");

                            if (!int.TryParse(Console.ReadLine(), out int searchCase))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto case 1;
                            }
                            List<Core.Agent> agents = await SearchByField_Steps(searchCase, token);
                            AgentsToConsole(agents);
                        //Выбор действия после поиска в БД.
                        ActionsAfterSearch:
                            if (agents.Count() > 0)
                            {
                                Console.WriteLine("Повторить поиск - 1, Выбрать номер (не ID) - 2, Отмена - 0.");
                            }
                            else
                            {
                                Console.WriteLine("Повторить поиск - 1, Отмена - 0.");
                            }

                            if (!int.TryParse(Console.ReadLine(), out int selectedCase))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto ActionsAfterSearch;
                            }

                            switch (selectedCase)
                            {
                                //Возврат к выбору поля для поиска в БД.
                                case 1:
                                    goto SearchBeforeUpdate;
                                case 2:
                                    if (agents.Count > 0)
                                    {
                                        Console.WriteLine("Введите номер (не ID) записи из списка найденных. Отмена - 0.");
                                        if (!int.TryParse(Console.ReadLine(), out int selectedNumber))
                                        {
                                            Console.WriteLine(CustomMessages.NotAnId);
                                            goto ActionsAfterSearch;
                                        }
                                        if (selectedNumber == 0)
                                        {
                                            goto ActionsAfterSearch;
                                        }
                                        else if (selectedNumber > agents.Count)
                                        {
                                            Console.WriteLine("Указанный номер отсутствует в списке найденных.");
                                            goto case 2;
                                        }
                                        else
                                        {
                                            //Вот тут мы наконец-то определилсь с выбором записи, которую будем удалять.
                                            //-1 так выбираем номер, а не индекс.
                                            Core.Agent agent = agents[selectedNumber - 1];
                                            await DeleteRecord_Steps(agent, token);
                                        }
                                    }
                                    else
                                    {
                                        goto default;
                                    }
                                    break;
                                //Возврат к самому началу этапа обновения записи.
                                case 0:
                                    continue;
                                default:
                                    Console.WriteLine(CustomMessages.UnknownId);
                                    goto ActionsAfterSearch;
                            }
                            break;

                        //Выбор записи по ID -> Удаление.
                        case 2:
                            Console.WriteLine("Введите ID записи из БД. Отмена - 0.");
                            if (!int.TryParse(Console.ReadLine(), out int recordId))
                            {
                                Console.WriteLine(CustomMessages.NotAnId);
                                goto case 2;
                            }
                            //Возврат к самому началу этапа обновения записи.
                            if (recordId == 0)
                            {
                                continue;
                            }
                            else
                            {
                                var searchById = await Core.Agent.Search_ById(recordId, token);
                                if (searchById.Count == 0)
                                {
                                    Console.WriteLine($"Запись с id = {searchById} не найдена.");
                                    goto case 2;
                                }
                                else
                                {
                                    //Вот тут мы наконец-то определилсь с выбором записи, которую будем удалять.
                                    //Поиск по id вернёт либо 0 либо 1, но чтобы не делать под еденичный результат доп. методы, оставил так.
                                    Core.Agent agent = searchById[0];
                                    await DeleteRecord_Steps(agent, token);
                                }
                            }
                            break;

                        case 0:
                            return;

                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static async Task DeleteRecord_Steps(Core.Agent agent, CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine($"====== Таблица Agent ====== Удаление записи ID = {agent.ID} ======");
                Console.WriteLine(AgentToSring(agent));
                Console.WriteLine("Удалить запись - 1, Отмена - 0.");
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                switch (idAction)
                {
                    case 1:
                        await Core.Agent.Delete_ById(agent.ID, token);
                        Console.WriteLine(CustomMessages.RecordDeleteSuccess);
                        return;
                    case 0:
                        return;
                    default:
                        Console.WriteLine(CustomMessages.UnknownId);
                        break;
                }
            }

        }

        private static void AgentsToConsole(List<Core.Agent> agents)
        {
            Console.WriteLine($"Найдено совпадений: {agents.Count}");
            int i = 1;

            agents.ForEach(x =>
            {
                Console.WriteLine($"{i:00}. {AgentToSring(x)}");
                //Console.WriteLine(String.Format(
                //    "{0}. Имя: {1, -15}, фамилия: {2, -15}, отчество: {3,-15}, ID отдела: {4}, " +
                //    "ID должности: {5}, ID звания: {6}, ID статуса: {7}, телефон: {8, -15}, адресс: {9}.",
                //    i.ToString("00"), x.FirstName, x.LastName, x.MiddleName, x.DepartmentID, x.PositionID, x.RankID, x.StatusID, x.Phone, x.Address));
                i++;
            });
        }

        private static string AgentToSring(Core.Agent agent)
        {
            return String.Format(
                    "Имя: {0, -15}, фамилия: {1, -15}, отчество: {2,-15}, ID отдела: {3}, " +
                    "ID должности: {4}, ID звания: {5}, ID статуса: {6}, телефон: {7, -15}, адресс: {8}.",
                    agent.FirstName, agent.LastName, agent.MiddleName, agent.DepartmentID, agent.PositionID, agent.RankID, agent.StatusID, agent.Phone, agent.Address);
        }
    }
}