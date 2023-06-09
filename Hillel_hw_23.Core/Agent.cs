﻿using Hillel_hw_23.Data;
using System.Text.Json.Serialization;

namespace Hillel_hw_23.Core
{
    public class Agent
    {
        private int id;
        private string firstName;
        private string lastName;
        private string? middleName;
        private int departmentID;
        private int positionID;
        private int rankId;
        private int statusId;
        private string? phone;
        private string? address;

        public int ID { get => id; set => id = value; }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Имя не должно быть пустым.") :
                    value.Length > 50 ? throw new ArgumentException("Имя должно быть менее 50 символов.") : value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Фамилия не должна быть пустой.") :
                    value.Length > 50 ? throw new ArgumentException("Фамилия должна быть не более 50 символов.") : value;
            }
        }
        public string? MiddleName
        {
            get => middleName;
            set
            {
                middleName = string.IsNullOrEmpty(value) ? null :
                    value.Length > 50 ? throw new ArgumentException("Отчество должно быть не более 50 символов.") : value;
            }
        }
        public int DepartmentID
        {
            get => departmentID;
            set
            {
                departmentID = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
            }
        }
        public int PositionID
        {
            get => positionID;
            set
            {
                positionID = value < 1 ? throw new ArgumentException("ID должности не может быть 0 или отрицательным.") : value;
            }
        }
        public int RankID
        {
            get => rankId;
            set
            {
                rankId = value < 1 ? throw new ArgumentException("ID звания не может быть 0 или отрицательным.") : value;
            }
        }
        public int StatusID
        {
            get => statusId;
            set
            {
                statusId = value < 1 ? throw new ArgumentException("ID статуса не может быть 0 или отрицательным.") : value;
            }
        }
        public string? Phone
        {
            get => phone;
            set
            {
                phone = string.IsNullOrEmpty(value) ? null :
                    value.Length > 20 ? throw new ArgumentException("Телефон должен быть менее 20 сиволов.") : value;
            }
        }
        public string? Address
        {
            get => address;
            set
            {
                address = string.IsNullOrEmpty(value) ? null :
                    value.Length > 100 ? throw new ArgumentException("Адрес должен быть менее 100 сиволов.") : value;
            }
        }

        public Agent() { }


        public Agent(int id, string fName, string lName, string? mName, int depId, int positionId, int rankId, int statusId, string? phone, string? address)
        {
            this.id = id;
            this.firstName = fName;
            this.lastName = lName;
            this.middleName = mName;
            this.departmentID = depId;
            this.positionID = positionId;
            this.rankId = rankId;
            this.statusId = statusId;
            this.phone = phone;
            this.address = address;
        }

        public static async Task AddNew(string fName, string lName, string mName, int depId, int positionId, int rankId, int statusId, string phone, string address, CancellationToken token)
        {
            await Data.Agent.AddNew(
                fName == string.Empty ? throw new ArgumentException("Name can`t be empty.") :
                    fName.Length > 50 ? throw new ArgumentException("Name must be below 50 symbols.") : fName,
                lName == string.Empty ? throw new ArgumentException("Last name can`t be empty.") :
                    lName.Length > 50 ? throw new ArgumentException("Last name must be below 50 symbols.") : lName,
                mName == string.Empty ? null :
                    mName.Length > 50 ? throw new ArgumentException("Middle name must be below 50 symbols.") : mName,
                depId,
                positionId,
                rankId,
                statusId,
                phone == string.Empty ? null : phone,
                address == string.Empty ? null :
                    address.Length > 100 ? throw new ArgumentException("Address must be below 100 symbols.") : address,
                token);
        }

        public static async Task AddNew(Agent agent, CancellationToken token)
        {
            await Data.Agent.AddNew(MapperToDataAgent(agent), token);
        }

        public static async Task<List<Agent>> ReadAll (CancellationToken token)
        {
            var rez = await Data.Agent.ReadAll(token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task<List<Agent>> Search_ById(int id, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ById(id, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task<List<Agent>> Search_ByFirstName(string fName, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ByFirstName(fName, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task<List<Agent>> Search_ByLastName(string lName, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ByLastName(lName, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task<List<Agent>> Search_ByDepartmentId(int depId, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ByDepartmentId(depId, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task<List<Agent>> Search_ByPositionId(int posId, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ByPositionId(posId, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }
        
        public static async Task<List<Agent>> Search_ByStatusId(int statusId, CancellationToken token)
        {
            var rez = await Data.Agent.Search_ByStatusId(statusId, token);
            return rez.Select(MapperFromDataAgent).ToList();
        }

        public static async Task Update(Agent agent, CancellationToken token)
        {
            await Data.Agent.Update(MapperToDataAgent(agent), token);
        }

        public static async Task Delete_ById(int id, CancellationToken token)
        {
            await Data.Agent.Delete_Byid(id, token);
        }

        private static Agent MapperFromDataAgent(Data.Agent agent)
        {
            return new Core.Agent(agent.ID, agent.FirstName, agent.LastName, agent.MiddleName, agent.DepartmentID, agent.PositionID, agent.RankID, agent.StatusID,
                agent.Phone, agent.Address);
        }

        private static Data.Agent MapperToDataAgent(Core.Agent agent)
        {
            return new Data.Agent(agent.ID,
                                agent.FirstName == string.Empty ? throw new ArgumentException("Имя не должно быть пустым.") :
                    agent.FirstName.Length > 50 ? throw new ArgumentException("Имя должно быть менее 50 символов.") : agent.FirstName,
                agent.LastName == string.Empty ? throw new ArgumentException("Фамилия не должна быть пустой.") :
                    agent.LastName.Length > 50 ? throw new ArgumentException("Фамилия должна быть не более 50 символов.") : agent.LastName,
                string.IsNullOrEmpty(agent.MiddleName) ? null :
                    agent.MiddleName.Length > 50 ? throw new ArgumentException("Отчество должно быть не более 50 символов.") : agent.MiddleName,
                agent.DepartmentID < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : agent.DepartmentID,
                agent.PositionID < 1 ? throw new ArgumentException("ID должности не может быть 0 или отрицательным.") : agent.PositionID,
                agent.RankID < 1 ? throw new ArgumentException("ID звания не может быть 0 или отрицательным.") : agent.RankID,
                agent.StatusID < 1 ? throw new ArgumentException("ID статуса не может быть 0 или отрицательным.") : agent.StatusID,
                string.IsNullOrEmpty(agent.Phone) ? null :
                    agent.Phone.Length > 20 ? throw new ArgumentException("Телефон должен быть менее 20 сиволов.") : agent.Phone,
                string.IsNullOrEmpty(agent.Address) ? null :
                    agent.Address.Length > 100 ? throw new ArgumentException("Адрес должен быть менее 100 сиволов.") : agent.Address);
        }
    }
}