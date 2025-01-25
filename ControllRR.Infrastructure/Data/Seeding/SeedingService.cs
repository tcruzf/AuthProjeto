using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControllRR.Infrastructure.Data.Seeding;


public class SeedingService
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
    using (var _context = new ControllRRContext(
        serviceProvider.GetRequiredService<DbContextOptions<ControllRRContext>>()))
        {
            if(_context.Users.Any() ||
             _context.Devices.Any() ||
             _context.Sectors.Any() ||
             _context.Maintenances.Any())
             {
                return;
             }
                Sector s1 = new Sector(1, "Setor 1", "Loc Setor 1", "Av Santos Dumont", "S/N", "Centro", "Janauba", "34201147", "Carlos Rafael");
                Sector s2 = new Sector(2, "Setor 2", "Loc Setor 2", "rua F ", "60", "Santana II", "Ouro Preto", "34201147", "Fernanda");
                Sector s3 = new Sector(3, "Setor 3", "Loc Setor 3", "rua gomes e santos ", "65", "Ouro branco", "Mariana", "34201147", "Maria");
                Sector s4 = new Sector(4, "Setor 4", "Loc Setor 4", "Rua pedro pereira", "650", "Centro", "São José", "34201147", "JOão pedro");
                Sector s5 = new Sector(5, "Setor 5", "Loc Setor 5", "Santos dias ", "600", "Ceramica", "Janauba", "34201147", "Maria");
                Sector s6 = new Sector(6, "Setor 6", "Loc Setor 6", "Avenida dos Anjos ", "12", "Alphaville", "Santos", "34201147", "Maria");
                Sector s7 = new Sector(7, "Setor 7", "Loc Setor 7", "rua das acacias ", "65", " Centro", "Santos", "34201147", "Maria");
                /*
                Sector s1 = new Sector(1, "Setor 1", "Loc Setor 1 ", "af" );
                Sector s2 = new Sector(2, "Setor 2", "Loc Setor 2", "aff");
                Sector s3 = new Sector(3, "Setor 3", "Loc Setor 3", "afff");
                Sector s4 = new Sector(4, "Setor 4", "Loc Setor 4", "affff");
                Sector s5 = new Sector(5, "Setor 5", "Loc Setor 5", "afffff");
                Sector s6 = new Sector(6, "Setor 6", "Loc Setor 6","afffff" );
                Sector s7 = new Sector(7, "Setor 7", "Loc Setor 7", "affffff");

                */
               //Device(int id, string type, string identifier, string model, string serialNumber, string description)
                Device dv1 = new Device(1,"PC Dell Conv.", "1574", "HP6325", s1, "301441", "Pc usado como servidor - alterar para um servidor dell depois");
                Device dv2 = new Device(2,"PC Servidor", "151321", "hs433", s2, "3012541", "Maquina de João");
                Device dv3 = new Device(3,"PC Servidor", "15151", "47Yh21", s3, "1244414", "maquina do setor de TI");
                Device dv4 = new Device(4,"PC Servidor", "15124", "65Myhg", s4, "8747", "Maquina nova destinada ao departamento/uso coletivo");
                Device dv5 = new Device(5,"PC Servidor", "15954", "ST00214", s5, "af574Yhg","Maquina removida do serviço de documentação");
                Device dv6 = new Device(6,"PC Servidor", "15224", "SM532", s6, "985AXHY", "Maquina destinada a doação");
                Device dv7 = new Device(7,"PC Servidor", "15223", "191874", s7, "MY567", "Maquina emprestada ao setor de ...");
                Device dv8 = new Device(8,"PC Positivo", "15521", "ST00214", s1, "985GTR", "maquina nova que veio com defeito");


                /*
                


                Device dv1 = new Device(1,"PC Dell Conv.", "1574", "HP6325", "301441", "Pc usado como servidor - alterar para um servidor dell depois");
                Device dv2 = new Device(2,"PC Servidor", "151321", "hs433",  "3012541", "Maquina de João");
                Device dv3 = new Device(3,"PC Servidor", "15151", "47Yh21",  "1244414", "maquina do setor de TI");
                Device dv4 = new Device(4,"PC Servidor", "15124", "65Myhg",  "8747", "Maquina nova destinada ao departamento/uso coletivo");
                Device dv5 = new Device(5,"PC Servidor", "15954", "ST00214", "af574Yhg","Maquina removida do serviço de documentação");
                Device dv6 = new Device(6,"PC Servidor", "15224", "SM532",  "985AXHY", "Maquina destinada a doação");
                Device dv7 = new Device(7,"PC Servidor", "15223", "191874",  "MY567", "Maquina emprestada ao setor de ...");
                Device dv8 = new Device(8,"PC Positivo", "15521", "ST00214", "985GTR", "maquina nova que veio com defeito");
                */
               // User(int id, string name, string phone, double register)
                User usr1 = new User(1, "Thiago Ferc", "38998235679", 17155);
                User usr2 = new User(2, "Gabriel Fernandes", "38999885544", 14111);
                User usr3 = new User(3, "Fernanda Vieira", "38555544141", 15412);
                User usr4 = new User(4, "Maria Aparecida", "996633254", 18190);
                User usr5 = new User(5, "Bruno Henrique", "38941253698", 19820);
                User usr6 = new User(6, "Pedro Carlos ", "38998235779", 13145);
                User usr7 = new User(7, "Pedro Antonio", "38998236579", 1828);
                User usr8 = new User(8, "Joao Paulo Silva", "38998235680", 1929);
                /*
                User usr1 = new User(1, "Thiago Ferc", "38998235679", 17155, new DateTime(2024, 09, 22));
                User usr2 = new User(2, "Gabriel Fernandes", "38999885544", 14111, new DateTime(2024, 05, 22));
                User usr3 = new User(3, "Fernanda Vieira", "38555544141", 15412, new DateTime(2025, 09, 22));
                User usr4 = new User(4, "Maria Aparecida", "996633254", 18190, new DateTime(2024, 10, 21));
                User usr5 = new User(5, "Bruno Henrique", "38941253698", 19820, new DateTime(2023, 02, 14));
                User usr6 = new User(6, "Pedro Carlos ", "38998235779", 13145, new DateTime(2022, 06, 10));
                User usr7 = new User(7, "Pedro Antonio", "38998236579", 1828, new DateTime(2024, 09, 22));
                User usr8 = new User(8, "Joao Paulo Silva", "38998235680", 1929, new DateTime(2024, 12, 31));

                
                */


                 //Maintenance(int id, string description, string simpleDesc, DateTime openDate,DateTime closeDate, MaintenanceStatus status, int maintenanceNumber )
               // Maintenance m1 = new Maintenance(1, "PC com problemas para ligar", "Erro bios" , new DateTime(2024, 09, 25), new DateTime(2024, 09, 30), MaintenanceStatus.Pendente,  443);
               // Maintenance m2 = new Maintenance(2, "Não liga", "asdfasdf", new DateTime(2024, 09, 22), new DateTime(2024, 09, 28), Domain.Enums.MaintenanceStatus.Finalizada,  7474 );
               // Maintenance m3 = new Maintenance(3, "PC molhou", "PC molhou", new DateTime(2024, 09, 10), new DateTime(2024, 09, 15), MaintenanceStatus.Finalizada,  7412 );
               // Maintenance m4 = new Maintenance(4, "PC pegou fogo após o coler parar de girar", "Pegou Fogo", new DateTime(2024, 09, 18), new DateTime(2024, 09, 28), MaintenanceStatus.Pendente, 1233);
               // Maintenance m5 = new Maintenance(5, "Formatar","Format", new DateTime(2024, 10, 18), new DateTime(2024, 10, 28), MaintenanceStatus.Aguardando, 11298 );
               
               Maintenance m1 = new Maintenance(1, "PC com problemas para ligar", "Erro bios" , new DateTime(2024, 09, 25), new DateTime(2024, 09, 30), MaintenanceStatus.Pendente, usr1, dv1, 1443);
                Maintenance m2 = new Maintenance(2, "Não liga", "asdfasdf", new DateTime(2024, 09, 22), new DateTime(2024, 09, 28), Domain.Enums.MaintenanceStatus.Finalizada, usr2, dv2, 7474 );
                Maintenance m3 = new Maintenance(3, "PC molhou", "PC molhou", new DateTime(2024, 09, 10), new DateTime(2024, 09, 15), MaintenanceStatus.Finalizada, usr3, dv3, 412 );
                Maintenance m4 = new Maintenance(4, "PC pegou fogo após o coler parar de girar", "Pegou Fogo", new DateTime(2024, 09, 18), new DateTime(2024, 09, 28), MaintenanceStatus.Pendente, usr4, dv4, 1233);
                Maintenance m5 = new Maintenance(5, "Formatar","Format", new DateTime(2024, 10, 18), new DateTime(2024, 10, 28), MaintenanceStatus.Aguardando, usr1, dv5, 11298 );
                
                _context.AddRange(usr1, usr2, usr3, usr4, usr5, usr6, usr7, usr8);
                _context.AddRange(s1, s2, s3, s4, s5, s6, s7);
                _context.AddRange(dv1, dv2, dv3, dv4, dv5, dv6, dv7, dv8);
                _context.AddRange(m1, m2, m3, m4, m5);
                _context.SaveChanges();
        
        }


    }
}