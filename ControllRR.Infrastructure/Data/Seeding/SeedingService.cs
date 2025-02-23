/*using System.Threading.Tasks;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControllRR.Infrastructure.Data.Seeding;


public class SeedingService
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var _context = new ControllRRContext(
            serviceProvider.GetRequiredService<DbContextOptions<ControllRRContext>>()))
        {
           ///<summary>
           ///      Checa se existem dados na base, usa especificamente o context para testar se alguma das
           ///      checagens retorna TRUE. Caso sim, não será feito o seed de dados.
           ///      Mesmo que uma tabela apenas tenha dados, as demais estão condicionadas ao teste individual daquela primeira.
           ///      Para fins de testes com o seed, recomendo que exclua a database(USE com CAUTELA) e em seguida rode o update
           ///      
           ///     Deleta o banco que possue informações:  /usr/bin/dotnet ef database drop --project ControllRR.Infrastructure
           ///     
           ///     Caso não haja migrations, recomendo que execute: 
           ///                  /usr/bin/dotnet ef migrations add InitialMigrations --project ControllRR.Infrastructure --output-dir Data/Migrations
           ///      
           ///                  
           ///     Recria o banco de dados com as informações abaixo: /usr/bin/dotnet ef database update --project ControllRR.Infrastructure
           /// 
           /// 
           ///</sumary>
       
            if (_context.Users.Any() ||
             _context.Devices.Any() ||
             _context.Sectors.Any() ||
             _context.Maintenances.Any())
            {
                // Caso alguma das condições acima retorne true, então nada será feito!
                return;
            }
        
            // Cria alguns itens de testes. Todos devem ser removidos, assim como as suas movimentações logo abaixo do bloco de itens 
            // do estoque.
            Stock st1 = new Stock(1, "Fonte Atx 500w", "Fonte para computador - Tipo: ATX", 30, "Desktop padrão ATX", "PC convencional");
            Stock st2 = new Stock(2, "Product_2", "Description for Product_2", 664, "General Application", "REF-2");
            Stock st3 = new Stock(3, "Product_3", "Description for Product_3", 743, "General Application", "REF-3");
            Stock st4 = new Stock(4, "Product_4", "Description for Product_4", 180, "General Application", "REF-4");
            Stock st5 = new Stock(5, "Product_5", "Description for Product_5", 526, "General Application", "REF-5");
            Stock st6 = new Stock(6, "Product_6", "Description for Product_6", 428, "General Application", "REF-6");
            Stock st7 = new Stock(7, "Product_7", "Description for Product_7", 1056, "General Application", "REF-7");
            Stock st8 = new Stock(8, "Product_8", "Description for Product_8", 101, "General Application", "REF-8");
            Stock st9 = new Stock(9, "Product_9", "Description for Product_9", 314, "General Application", "REF-9");
            Stock st10 = new Stock(10, "Product_10", "Description for Product_10", 788, "General Application", "REF-10");
            Stock st11 = new Stock(11, "Product_11", "Description for Product_11", 481, "General Application", "REF-11");
            Stock st12 = new Stock(12, "Product_12", "Description for Product_12", 942, "General Application", "REF-12");
            Stock st13 = new Stock(13, "Product_13", "Description for Product_13", 680, "General Application", "REF-13");
            Stock st14 = new Stock(14, "Product_14", "Description for Product_14", 643, "General Application", "REF-14");
            Stock st15 = new Stock(15, "Product_15", "Description for Product_15", 119, "General Application", "REF-15");
            Stock st16 = new Stock(16, "Product_16", "Description for Product_16", 115, "General Application", "REF-16");
            Stock st17 = new Stock(17, "Product_17", "Description for Product_17", 452, "General Application", "REF-17");
            Stock st18 = new Stock(18, "Product_18", "Description for Product_18", 911, "General Application", "REF-18");
            Stock st19 = new Stock(19, "Product_19", "Description for Product_19", 131, "General Application", "REF-19");
            Stock st20 = new Stock(20, "Product_20", "Description for Product_20", 933, "General Application", "REF-20");
            Stock st21 = new Stock(21, "Product_21", "Description for Product_21", 757, "General Application", "REF-21");
            Stock st22 = new Stock(22, "Product_22", "Description for Product_22", 684, "General Application", "REF-22");
            Stock st23 = new Stock(23, "Product_23", "Description for Product_23", 1025, "General Application", "REF-23");
            Stock st24 = new Stock(24, "Product_24", "Description for Product_24", 750, "General Application", "REF-24");
            Stock st25 = new Stock(25, "Product_25", "Description for Product_25", 404, "General Application", "REF-25");
            Stock st26 = new Stock(26, "Product_26", "Description for Product_26", 1070, "General Application", "REF-26");
            Stock st27 = new Stock(27, "Product_27", "Description for Product_27", 949, "General Application", "REF-27");
            Stock st28 = new Stock(28, "Product_28", "Description for Product_28", 189, "General Application", "REF-28");
            Stock st29 = new Stock(29, "Product_29", "Description for Product_29", 108, "General Application", "REF-29");
            Stock st30 = new Stock(30, "Product_30", "Description for Product_30", 901, "General Application", "REF-30");
            Stock st31 = new Stock(31, "Product_31", "Description for Product_31", 377, "General Application", "REF-31");
            Stock st32 = new Stock(32, "Product_32", "Description for Product_32", 824, "General Application", "REF-32");
            Stock st33 = new Stock(33, "Product_33", "Description for Product_33", 1040, "General Application", "REF-33");
            Stock st34 = new Stock(34, "Product_34", "Description for Product_34", 642, "General Application", "REF-34");
            Stock st35 = new Stock(35, "Product_35", "Description for Product_35", 426, "General Application", "REF-35");
            Stock st36 = new Stock(36, "Product_36", "Description for Product_36", 769, "General Application", "REF-36");
            Stock st37 = new Stock(37, "Product_37", "Description for Product_37", 750, "General Application", "REF-37");
            Stock st38 = new Stock(38, "Product_38", "Description for Product_38", 671, "General Application", "REF-38");
            Stock st39 = new Stock(39, "Product_39", "Description for Product_39", 707, "General Application", "REF-39");
            Stock st40 = new Stock(40, "Product_40", "Description for Product_40", 211, "General Application", "REF-40");
            Stock st41 = new Stock(41, "Product_41", "Description for Product_41", 1065, "General Application", "REF-41");
            Stock st42 = new Stock(42, "Product_42", "Description for Product_42", 235, "General Application", "REF-42");
            Stock st43 = new Stock(43, "Product_43", "Description for Product_43", 118, "General Application", "REF-43");
            Stock st44 = new Stock(44, "Product_44", "Description for Product_44", 663, "General Application", "REF-44");
            Stock st45 = new Stock(45, "Product_45", "Description for Product_45", 358, "General Application", "REF-45");
            Stock st46 = new Stock(46, "Product_46", "Description for Product_46", 682, "General Application", "REF-46");
            Stock st47 = new Stock(47, "Product_47", "Description for Product_47", 248, "General Application", "REF-47");
            Stock st48 = new Stock(48, "Product_48", "Description for Product_48", 1090, "General Application", "REF-48");
            Stock st49 = new Stock(49, "Product_49", "Description for Product_49", 1021, "General Application", "REF-49");
            Stock st50 = new Stock(50, "Product_50", "Description for Product_50", 575, "General Application", "REF-50");
            Stock st51 = new Stock(51, "Product_51", "Description for Product_51", 270, "General Application", "REF-51");
            Stock st52 = new Stock(52, "Product_52", "Description for Product_52", 800, "General Application", "REF-52");
            Stock st53 = new Stock(53, "Product_53", "Description for Product_53", 610, "General Application", "REF-53");
            Stock st54 = new Stock(54, "Product_54", "Description for Product_54", 293, "General Application", "REF-54");
            Stock st55 = new Stock(55, "Product_55", "Description for Product_55", 640, "General Application", "REF-55");
            Stock st56 = new Stock(56, "Product_56", "Description for Product_56", 958, "General Application", "REF-56");
            Stock st57 = new Stock(57, "Product_57", "Description for Product_57", 590, "General Application", "REF-57");
            Stock st58 = new Stock(58, "Product_58", "Description for Product_58", 537, "General Application", "REF-58");
            Stock st59 = new Stock(59, "Product_59", "Description for Product_59", 289, "General Application", "REF-59");
            Stock st60 = new Stock(60, "Product_60", "Description for Product_60", 1040, "General Application", "REF-60");
            Stock st61 = new Stock(61, "Product_61", "Description for Product_61", 1051, "General Application", "REF-61");
            Stock st62 = new Stock(62, "Product_62", "Description for Product_62", 771, "General Application", "REF-62");
            Stock st63 = new Stock(63, "Product_63", "Description for Product_63", 332, "General Application", "REF-63");
            Stock st64 = new Stock(64, "Product_64", "Description for Product_64", 955, "General Application", "REF-64");
            Stock st65 = new Stock(65, "Product_65", "Description for Product_65", 351, "General Application", "REF-65");
            Stock st66 = new Stock(66, "Product_66", "Description for Product_66", 217, "General Application", "REF-66");
            Stock st67 = new Stock(67, "Product_67", "Description for Product_67", 922, "General Application", "REF-67");
            Stock st68 = new Stock(68, "Product_68", "Description for Product_68", 943, "General Application", "REF-68");
            Stock st69 = new Stock(69, "Product_69", "Description for Product_69", 406, "General Application", "REF-69");
            Stock st70 = new Stock(70, "Product_70", "Description for Product_70", 122, "General Application", "REF-70");
            Stock st71 = new Stock(71, "Product_71", "Description for Product_71", 242, "General Application", "REF-71");
            Stock st72 = new Stock(72, "Product_72", "Description for Product_72", 419, "General Application", "REF-72");
            Stock st73 = new Stock(73, "Product_73", "Description for Product_73", 866, "General Application", "REF-73");
            Stock st74 = new Stock(74, "Product_74", "Description for Product_74", 630, "General Application", "REF-74");
            Stock st75 = new Stock(75, "Product_75", "Description for Product_75", 199, "General Application", "REF-75");
            Stock st76 = new Stock(76, "Product_76", "Description for Product_76", 281, "General Application", "REF-76");
            Stock st77 = new Stock(77, "Product_77", "Description for Product_77", 560, "General Application", "REF-77");
            Stock st78 = new Stock(78, "Product_78", "Description for Product_78", 543, "General Application", "REF-78");
            Stock st79 = new Stock(79, "Product_79", "Description for Product_79", 624, "General Application", "REF-79");
            Stock st80 = new Stock(80, "Product_80", "Description for Product_80", 656, "General Application", "REF-80");
            Stock st81 = new Stock(81, "Product_81", "Description for Product_81", 978, "General Application", "REF-81");
            Stock st82 = new Stock(82, "Product_82", "Description for Product_82", 611, "General Application", "REF-82");
            Stock st83 = new Stock(83, "Product_83", "Description for Product_83", 426, "General Application", "REF-83");
            Stock st84 = new Stock(84, "Product_84", "Description for Product_84", 478, "General Application", "REF-84");
            Stock st85 = new Stock(85, "Product_85", "Description for Product_85", 663, "General Application", "REF-85");
            Stock st86 = new Stock(86, "Product_86", "Description for Product_86", 117, "General Application", "REF-86");
            Stock st87 = new Stock(87, "Product_87", "Description for Product_87", 273, "General Application", "REF-87");
            Stock st88 = new Stock(88, "Product_88", "Description for Product_88", 188, "General Application", "REF-88");
            Stock st89 = new Stock(89, "Product_89", "Description for Product_89", 359, "General Application", "REF-89");
            Stock st90 = new Stock(90, "Product_90", "Description for Product_90", 278, "General Application", "REF-90");
            Stock st91 = new Stock(91, "Product_91", "Description for Product_91", 148, "General Application", "REF-91");
            Stock st92 = new Stock(92, "Product_92", "Description for Product_92", 373, "General Application", "REF-92");
            Stock st93 = new Stock(93, "Product_93", "Description for Product_93", 673, "General Application", "REF-93");
            Stock st94 = new Stock(94, "Product_94", "Description for Product_94", 731, "General Application", "REF-94");
            Stock st95 = new Stock(95, "Product_95", "Description for Product_95", 755, "General Application", "REF-95");
            Stock st96 = new Stock(96, "Product_96", "Description for Product_96", 548, "General Application", "REF-96");
            Stock st97 = new Stock(97, "Product_97", "Description for Product_97", 113, "General Application", "REF-97");
            Stock st98 = new Stock(98, "Product_98", "Description for Product_98", 953, "General Application", "REF-98");
            Stock st99 = new Stock(99, "Product_99", "Description for Product_99", 906, "General Application", "REF-99");
            Stock st100 = new Stock(100, "Product_100", "Description for Product_100", 761, "General Application", "REF-100");

            // StockManagement stmgt = new StockManagement(1, new DateTime(2024, 09, 10), StockMovementType.Entrada, 30, st1);
            StockManagement stmgt1 = new StockManagement(1, new DateTime(2023, 1, 1), StockMovementType.Saida, 33, st1);
            StockManagement stmgt2 = new StockManagement(2, new DateTime(2023, 1, 2), StockMovementType.Entrada, 18, st1);
            StockManagement stmgt3 = new StockManagement(3, new DateTime(2023, 1, 3), StockMovementType.Saida, 53, st1);
            StockManagement stmgt4 = new StockManagement(4, new DateTime(2023, 1, 4), StockMovementType.Entrada, 25, st1);
            StockManagement stmgt5 = new StockManagement(5, new DateTime(2023, 1, 5), StockMovementType.Saida, 34, st1);
            StockManagement stmgt6 = new StockManagement(6, new DateTime(2023, 1, 6), StockMovementType.Saida, 14, st2);
            StockManagement stmgt7 = new StockManagement(7, new DateTime(2023, 1, 7), StockMovementType.Entrada, 37, st2);
            StockManagement stmgt8 = new StockManagement(8, new DateTime(2023, 1, 8), StockMovementType.Saida, 34, st2);
            StockManagement stmgt9 = new StockManagement(9, new DateTime(2023, 1, 9), StockMovementType.Entrada, 31, st2);
            StockManagement stmgt10 = new StockManagement(10, new DateTime(2023, 1, 10), StockMovementType.Saida, 44, st2);
            StockManagement stmgt11 = new StockManagement(11, new DateTime(2023, 1, 11), StockMovementType.Saida, 20, st3);
            StockManagement stmgt12 = new StockManagement(12, new DateTime(2023, 1, 12), StockMovementType.Entrada, 58, st3);
            StockManagement stmgt13 = new StockManagement(13, new DateTime(2023, 1, 13), StockMovementType.Saida, 54, st3);
            StockManagement stmgt14 = new StockManagement(14, new DateTime(2023, 1, 14), StockMovementType.Entrada, 47, st3);
            StockManagement stmgt15 = new StockManagement(15, new DateTime(2023, 1, 15), StockMovementType.Saida, 54, st3);
            StockManagement stmgt16 = new StockManagement(16, new DateTime(2023, 1, 16), StockMovementType.Saida, 30, st4);
            StockManagement stmgt17 = new StockManagement(17, new DateTime(2023, 1, 17), StockMovementType.Entrada, 39, st4);
            StockManagement stmgt18 = new StockManagement(18, new DateTime(2023, 1, 18), StockMovementType.Saida, 45, st4);
            StockManagement stmgt19 = new StockManagement(19, new DateTime(2023, 1, 19), StockMovementType.Entrada, 42, st4);
            StockManagement stmgt20 = new StockManagement(20, new DateTime(2023, 1, 20), StockMovementType.Saida, 10, st4);
            StockManagement stmgt21 = new StockManagement(21, new DateTime(2023, 1, 21), StockMovementType.Saida, 20, st5);
            StockManagement stmgt22 = new StockManagement(22, new DateTime(2023, 1, 22), StockMovementType.Entrada, 38, st5);
            StockManagement stmgt23 = new StockManagement(23, new DateTime(2023, 1, 23), StockMovementType.Saida, 29, st5);
            StockManagement stmgt24 = new StockManagement(24, new DateTime(2023, 1, 24), StockMovementType.Entrada, 28, st5);
            StockManagement stmgt25 = new StockManagement(25, new DateTime(2023, 1, 25), StockMovementType.Saida, 32, st5);
            StockManagement stmgt26 = new StockManagement(26, new DateTime(2023, 1, 26), StockMovementType.Saida, 59, st6);
            StockManagement stmgt27 = new StockManagement(27, new DateTime(2023, 1, 27), StockMovementType.Entrada, 47, st6);
            StockManagement stmgt28 = new StockManagement(28, new DateTime(2023, 1, 28), StockMovementType.Saida, 59, st6);
            StockManagement stmgt29 = new StockManagement(29, new DateTime(2023, 2, 1), StockMovementType.Entrada, 47, st6);
            StockManagement stmgt30 = new StockManagement(30, new DateTime(2023, 2, 2), StockMovementType.Saida, 20, st6);
            StockManagement stmgt31 = new StockManagement(31, new DateTime(2023, 2, 3), StockMovementType.Saida, 51, st7);
            StockManagement stmgt32 = new StockManagement(32, new DateTime(2023, 2, 4), StockMovementType.Entrada, 39, st7);
            StockManagement stmgt33 = new StockManagement(33, new DateTime(2023, 2, 5), StockMovementType.Saida, 39, st7);
            StockManagement stmgt34 = new StockManagement(34, new DateTime(2023, 2, 6), StockMovementType.Entrada, 56, st7);
            StockManagement stmgt35 = new StockManagement(35, new DateTime(2023, 2, 7), StockMovementType.Saida, 19, st7);
            StockManagement stmgt36 = new StockManagement(36, new DateTime(2023, 2, 8), StockMovementType.Saida, 47, st8);
            StockManagement stmgt37 = new StockManagement(37, new DateTime(2023, 2, 9), StockMovementType.Entrada, 13, st8);
            StockManagement stmgt38 = new StockManagement(38, new DateTime(2023, 2, 10), StockMovementType.Saida, 11, st8);
            StockManagement stmgt39 = new StockManagement(39, new DateTime(2023, 2, 11), StockMovementType.Entrada, 16, st8);
            StockManagement stmgt40 = new StockManagement(40, new DateTime(2023, 2, 12), StockMovementType.Saida, 35, st8);
            StockManagement stmgt41 = new StockManagement(41, new DateTime(2023, 2, 13), StockMovementType.Saida, 44, st9);
            StockManagement stmgt42 = new StockManagement(42, new DateTime(2023, 2, 14), StockMovementType.Entrada, 45, st9);
            StockManagement stmgt43 = new StockManagement(43, new DateTime(2023, 2, 15), StockMovementType.Saida, 37, st9);
            StockManagement stmgt44 = new StockManagement(44, new DateTime(2023, 2, 16), StockMovementType.Entrada, 13, st9);
            StockManagement stmgt45 = new StockManagement(45, new DateTime(2023, 2, 17), StockMovementType.Saida, 53, st9);
            StockManagement stmgt46 = new StockManagement(46, new DateTime(2023, 2, 18), StockMovementType.Saida, 19, st10);
            StockManagement stmgt47 = new StockManagement(47, new DateTime(2023, 2, 19), StockMovementType.Entrada, 39, st10);
            StockManagement stmgt48 = new StockManagement(48, new DateTime(2023, 2, 20), StockMovementType.Saida, 52, st10);
            StockManagement stmgt49 = new StockManagement(49, new DateTime(2023, 2, 21), StockMovementType.Entrada, 26, st10);
            StockManagement stmgt50 = new StockManagement(50, new DateTime(2023, 2, 22), StockMovementType.Saida, 38, st10);
            StockManagement stmgt51 = new StockManagement(51, new DateTime(2023, 2, 23), StockMovementType.Saida, 33, st11);
            StockManagement stmgt52 = new StockManagement(52, new DateTime(2023, 2, 24), StockMovementType.Entrada, 57, st11);
            StockManagement stmgt53 = new StockManagement(53, new DateTime(2023, 2, 25), StockMovementType.Saida, 58, st11);
            StockManagement stmgt54 = new StockManagement(54, new DateTime(2023, 2, 26), StockMovementType.Entrada, 19, st11);
            StockManagement stmgt55 = new StockManagement(55, new DateTime(2023, 2, 27), StockMovementType.Saida, 17, st11);
            StockManagement stmgt56 = new StockManagement(56, new DateTime(2023, 2, 28), StockMovementType.Saida, 23, st12);
            StockManagement stmgt57 = new StockManagement(57, new DateTime(2023, 3, 1), StockMovementType.Entrada, 43, st12);
            StockManagement stmgt58 = new StockManagement(58, new DateTime(2023, 3, 2), StockMovementType.Saida, 46, st12);
            StockManagement stmgt59 = new StockManagement(59, new DateTime(2023, 3, 3), StockMovementType.Entrada, 11, st12);
            StockManagement stmgt60 = new StockManagement(60, new DateTime(2023, 3, 4), StockMovementType.Saida, 20, st12);
            StockManagement stmgt61 = new StockManagement(61, new DateTime(2023, 3, 5), StockMovementType.Saida, 41, st13);
            StockManagement stmgt62 = new StockManagement(62, new DateTime(2023, 3, 6), StockMovementType.Entrada, 15, st13);
            StockManagement stmgt63 = new StockManagement(63, new DateTime(2023, 3, 7), StockMovementType.Saida, 52, st13);
            StockManagement stmgt64 = new StockManagement(64, new DateTime(2023, 3, 8), StockMovementType.Entrada, 27, st13);
            StockManagement stmgt65 = new StockManagement(65, new DateTime(2023, 3, 9), StockMovementType.Saida, 40, st13);
            StockManagement stmgt66 = new StockManagement(66, new DateTime(2023, 3, 10), StockMovementType.Saida, 19, st14);
            StockManagement stmgt67 = new StockManagement(67, new DateTime(2023, 3, 11), StockMovementType.Entrada, 44, st14);
            StockManagement stmgt68 = new StockManagement(68, new DateTime(2023, 3, 12), StockMovementType.Saida, 38, st14);
            StockManagement stmgt69 = new StockManagement(69, new DateTime(2023, 3, 13), StockMovementType.Entrada, 48, st14);
            StockManagement stmgt70 = new StockManagement(70, new DateTime(2023, 3, 14), StockMovementType.Saida, 47, st14);
            StockManagement stmgt71 = new StockManagement(71, new DateTime(2023, 3, 15), StockMovementType.Saida, 41, st15);
            StockManagement stmgt72 = new StockManagement(72, new DateTime(2023, 3, 16), StockMovementType.Entrada, 13, st15);
            StockManagement stmgt73 = new StockManagement(73, new DateTime(2023, 3, 17), StockMovementType.Saida, 56, st15);
            StockManagement stmgt74 = new StockManagement(74, new DateTime(2023, 3, 18), StockMovementType.Entrada, 44, st15);
            StockManagement stmgt75 = new StockManagement(75, new DateTime(2023, 3, 19), StockMovementType.Saida, 12, st15);
            StockManagement stmgt76 = new StockManagement(76, new DateTime(2023, 3, 20), StockMovementType.Saida, 43, st16);
            StockManagement stmgt77 = new StockManagement(77, new DateTime(2023, 3, 21), StockMovementType.Entrada, 10, st16);
            StockManagement stmgt78 = new StockManagement(78, new DateTime(2023, 3, 22), StockMovementType.Saida, 21, st16);
            StockManagement stmgt79 = new StockManagement(79, new DateTime(2023, 3, 23), StockMovementType.Entrada, 54, st16);
            StockManagement stmgt80 = new StockManagement(80, new DateTime(2023, 3, 24), StockMovementType.Saida, 43, st16);
            StockManagement stmgt81 = new StockManagement(81, new DateTime(2023, 3, 25), StockMovementType.Saida, 10, st17);
            StockManagement stmgt82 = new StockManagement(82, new DateTime(2023, 3, 26), StockMovementType.Entrada, 44, st17);
            StockManagement stmgt83 = new StockManagement(83, new DateTime(2023, 3, 27), StockMovementType.Saida, 54, st17);
            StockManagement stmgt84 = new StockManagement(84, new DateTime(2023, 3, 28), StockMovementType.Entrada, 51, st17);
            StockManagement stmgt85 = new StockManagement(85, new DateTime(2023, 4, 1), StockMovementType.Saida, 28, st17);
            StockManagement stmgt86 = new StockManagement(86, new DateTime(2023, 4, 2), StockMovementType.Saida, 59, st18);
            StockManagement stmgt87 = new StockManagement(87, new DateTime(2023, 4, 3), StockMovementType.Entrada, 13, st18);
            StockManagement stmgt88 = new StockManagement(88, new DateTime(2023, 4, 4), StockMovementType.Saida, 16, st18);
            StockManagement stmgt89 = new StockManagement(89, new DateTime(2023, 4, 5), StockMovementType.Entrada, 57, st18);
            StockManagement stmgt90 = new StockManagement(90, new DateTime(2023, 4, 6), StockMovementType.Saida, 48, st18);
            StockManagement stmgt91 = new StockManagement(91, new DateTime(2023, 4, 7), StockMovementType.Saida, 38, st19);
            StockManagement stmgt92 = new StockManagement(92, new DateTime(2023, 4, 8), StockMovementType.Entrada, 38, st19);
            StockManagement stmgt93 = new StockManagement(93, new DateTime(2023, 4, 9), StockMovementType.Saida, 57, st19);
            StockManagement stmgt94 = new StockManagement(94, new DateTime(2023, 4, 10), StockMovementType.Entrada, 22, st19);
            StockManagement stmgt95 = new StockManagement(95, new DateTime(2023, 4, 11), StockMovementType.Saida, 41, st19);
            StockManagement stmgt96 = new StockManagement(96, new DateTime(2023, 4, 12), StockMovementType.Saida, 24, st20);
            StockManagement stmgt97 = new StockManagement(97, new DateTime(2023, 4, 13), StockMovementType.Entrada, 17, st20);
            StockManagement stmgt98 = new StockManagement(98, new DateTime(2023, 4, 14), StockMovementType.Saida, 28, st20);
            StockManagement stmgt99 = new StockManagement(99, new DateTime(2023, 4, 15), StockMovementType.Entrada, 41, st20);
            StockManagement stmgt100 = new StockManagement(100, new DateTime(2023, 4, 16), StockMovementType.Saida, 40, st20);
            StockManagement stmgt101 = new StockManagement(101, new DateTime(2023, 4, 17), StockMovementType.Saida, 30, st21);
            StockManagement stmgt102 = new StockManagement(102, new DateTime(2023, 4, 18), StockMovementType.Entrada, 25, st21);
            StockManagement stmgt103 = new StockManagement(103, new DateTime(2023, 4, 19), StockMovementType.Saida, 18, st21);
            StockManagement stmgt104 = new StockManagement(104, new DateTime(2023, 4, 20), StockMovementType.Entrada, 35, st21);
            StockManagement stmgt105 = new StockManagement(105, new DateTime(2023, 4, 21), StockMovementType.Saida, 57, st21);
            StockManagement stmgt106 = new StockManagement(106, new DateTime(2023, 4, 22), StockMovementType.Saida, 31, st22);
            StockManagement stmgt107 = new StockManagement(107, new DateTime(2023, 4, 23), StockMovementType.Entrada, 36, st22);
            StockManagement stmgt108 = new StockManagement(108, new DateTime(2023, 4, 24), StockMovementType.Saida, 55, st22);
            StockManagement stmgt109 = new StockManagement(109, new DateTime(2023, 4, 25), StockMovementType.Entrada, 56, st22);
            StockManagement stmgt110 = new StockManagement(110, new DateTime(2023, 4, 26), StockMovementType.Saida, 56, st22);
            StockManagement stmgt111 = new StockManagement(111, new DateTime(2023, 4, 27), StockMovementType.Saida, 13, st23);
            StockManagement stmgt112 = new StockManagement(112, new DateTime(2023, 4, 28), StockMovementType.Entrada, 19, st23);
            StockManagement stmgt113 = new StockManagement(113, new DateTime(2023, 5, 1), StockMovementType.Saida, 16, st23);
            StockManagement stmgt114 = new StockManagement(114, new DateTime(2023, 5, 2), StockMovementType.Entrada, 30, st23);
            StockManagement stmgt115 = new StockManagement(115, new DateTime(2023, 5, 3), StockMovementType.Saida, 38, st23);
            StockManagement stmgt116 = new StockManagement(116, new DateTime(2023, 5, 4), StockMovementType.Saida, 38, st24);
            StockManagement stmgt117 = new StockManagement(117, new DateTime(2023, 5, 5), StockMovementType.Entrada, 14, st24);
            StockManagement stmgt118 = new StockManagement(118, new DateTime(2023, 5, 6), StockMovementType.Saida, 52, st24);
            StockManagement stmgt119 = new StockManagement(119, new DateTime(2023, 5, 7), StockMovementType.Entrada, 14, st24);
            StockManagement stmgt120 = new StockManagement(120, new DateTime(2023, 5, 8), StockMovementType.Saida, 17, st24);
            StockManagement stmgt121 = new StockManagement(121, new DateTime(2023, 5, 9), StockMovementType.Saida, 23, st25);
            StockManagement stmgt122 = new StockManagement(122, new DateTime(2023, 5, 10), StockMovementType.Entrada, 38, st25);
            StockManagement stmgt123 = new StockManagement(123, new DateTime(2023, 5, 11), StockMovementType.Saida, 34, st25);
            StockManagement stmgt124 = new StockManagement(124, new DateTime(2023, 5, 12), StockMovementType.Entrada, 30, st25);
            StockManagement stmgt125 = new StockManagement(125, new DateTime(2023, 5, 13), StockMovementType.Saida, 11, st25);
            StockManagement stmgt126 = new StockManagement(126, new DateTime(2023, 5, 14), StockMovementType.Saida, 58, st26);
            StockManagement stmgt127 = new StockManagement(127, new DateTime(2023, 5, 15), StockMovementType.Entrada, 11, st26);
            StockManagement stmgt128 = new StockManagement(128, new DateTime(2023, 5, 16), StockMovementType.Saida, 22, st26);
            StockManagement stmgt129 = new StockManagement(129, new DateTime(2023, 5, 17), StockMovementType.Entrada, 49, st26);
            StockManagement stmgt130 = new StockManagement(130, new DateTime(2023, 5, 18), StockMovementType.Saida, 44, st26);
            StockManagement stmgt131 = new StockManagement(131, new DateTime(2023, 5, 19), StockMovementType.Saida, 22, st27);
            StockManagement stmgt132 = new StockManagement(132, new DateTime(2023, 5, 20), StockMovementType.Entrada, 56, st27);
            StockManagement stmgt133 = new StockManagement(133, new DateTime(2023, 5, 21), StockMovementType.Saida, 36, st27);
            StockManagement stmgt134 = new StockManagement(134, new DateTime(2023, 5, 22), StockMovementType.Entrada, 46, st27);
            StockManagement stmgt135 = new StockManagement(135, new DateTime(2023, 5, 23), StockMovementType.Saida, 14, st27);
            StockManagement stmgt136 = new StockManagement(136, new DateTime(2023, 5, 24), StockMovementType.Saida, 11, st28);
            StockManagement stmgt137 = new StockManagement(137, new DateTime(2023, 5, 25), StockMovementType.Entrada, 14, st28);
            StockManagement stmgt138 = new StockManagement(138, new DateTime(2023, 5, 26), StockMovementType.Saida, 23, st28);
            StockManagement stmgt139 = new StockManagement(139, new DateTime(2023, 5, 27), StockMovementType.Entrada, 22, st28);
            StockManagement stmgt140 = new StockManagement(140, new DateTime(2023, 5, 28), StockMovementType.Saida, 38, st28);
            StockManagement stmgt141 = new StockManagement(141, new DateTime(2023, 6, 1), StockMovementType.Saida, 36, st29);
            StockManagement stmgt142 = new StockManagement(142, new DateTime(2023, 6, 2), StockMovementType.Entrada, 32, st29);
            StockManagement stmgt143 = new StockManagement(143, new DateTime(2023, 6, 3), StockMovementType.Saida, 33, st29);
            StockManagement stmgt144 = new StockManagement(144, new DateTime(2023, 6, 4), StockMovementType.Entrada, 47, st29);
            StockManagement stmgt145 = new StockManagement(145, new DateTime(2023, 6, 5), StockMovementType.Saida, 40, st29);
            StockManagement stmgt146 = new StockManagement(146, new DateTime(2023, 6, 6), StockMovementType.Saida, 47, st30);
            StockManagement stmgt147 = new StockManagement(147, new DateTime(2023, 6, 7), StockMovementType.Entrada, 15, st30);
            StockManagement stmgt148 = new StockManagement(148, new DateTime(2023, 6, 8), StockMovementType.Saida, 32, st30);
            StockManagement stmgt149 = new StockManagement(149, new DateTime(2023, 6, 9), StockMovementType.Entrada, 37, st30);
            StockManagement stmgt150 = new StockManagement(150, new DateTime(2023, 6, 10), StockMovementType.Saida, 33, st30);
            StockManagement stmgt151 = new StockManagement(151, new DateTime(2023, 6, 11), StockMovementType.Saida, 49, st31);
            StockManagement stmgt152 = new StockManagement(152, new DateTime(2023, 6, 12), StockMovementType.Entrada, 12, st31);
            StockManagement stmgt153 = new StockManagement(153, new DateTime(2023, 6, 13), StockMovementType.Saida, 25, st31);
            StockManagement stmgt154 = new StockManagement(154, new DateTime(2023, 6, 14), StockMovementType.Entrada, 28, st31);
            StockManagement stmgt155 = new StockManagement(155, new DateTime(2023, 6, 15), StockMovementType.Saida, 35, st31);
            StockManagement stmgt156 = new StockManagement(156, new DateTime(2023, 6, 16), StockMovementType.Saida, 46, st32);
            StockManagement stmgt157 = new StockManagement(157, new DateTime(2023, 6, 17), StockMovementType.Entrada, 12, st32);
            StockManagement stmgt158 = new StockManagement(158, new DateTime(2023, 6, 18), StockMovementType.Saida, 25, st32);
            StockManagement stmgt159 = new StockManagement(159, new DateTime(2023, 6, 19), StockMovementType.Entrada, 19, st32);
            StockManagement stmgt160 = new StockManagement(160, new DateTime(2023, 6, 20), StockMovementType.Saida, 18, st32);
            StockManagement stmgt161 = new StockManagement(161, new DateTime(2023, 6, 21), StockMovementType.Saida, 41, st33);
            StockManagement stmgt162 = new StockManagement(162, new DateTime(2023, 6, 22), StockMovementType.Entrada, 57, st33);
            StockManagement stmgt163 = new StockManagement(163, new DateTime(2023, 6, 23), StockMovementType.Saida, 36, st33);
            StockManagement stmgt164 = new StockManagement(164, new DateTime(2023, 6, 24), StockMovementType.Entrada, 15, st33);
            StockManagement stmgt165 = new StockManagement(165, new DateTime(2023, 6, 25), StockMovementType.Saida, 12, st33);
            StockManagement stmgt166 = new StockManagement(166, new DateTime(2023, 6, 26), StockMovementType.Saida, 18, st34);
            StockManagement stmgt167 = new StockManagement(167, new DateTime(2023, 6, 27), StockMovementType.Entrada, 27, st34);
            StockManagement stmgt168 = new StockManagement(168, new DateTime(2023, 6, 28), StockMovementType.Saida, 16, st34);
            StockManagement stmgt169 = new StockManagement(169, new DateTime(2023, 7, 1), StockMovementType.Entrada, 42, st34);
            StockManagement stmgt170 = new StockManagement(170, new DateTime(2023, 7, 2), StockMovementType.Saida, 35, st34);
            StockManagement stmgt171 = new StockManagement(171, new DateTime(2023, 7, 3), StockMovementType.Saida, 46, st35);
            StockManagement stmgt172 = new StockManagement(172, new DateTime(2023, 7, 4), StockMovementType.Entrada, 14, st35);
            StockManagement stmgt173 = new StockManagement(173, new DateTime(2023, 7, 5), StockMovementType.Saida, 10, st35);
            StockManagement stmgt174 = new StockManagement(174, new DateTime(2023, 7, 6), StockMovementType.Entrada, 33, st35);
            StockManagement stmgt175 = new StockManagement(175, new DateTime(2023, 7, 7), StockMovementType.Saida, 39, st35);
            StockManagement stmgt176 = new StockManagement(176, new DateTime(2023, 7, 8), StockMovementType.Saida, 41, st36);
            StockManagement stmgt177 = new StockManagement(177, new DateTime(2023, 7, 9), StockMovementType.Entrada, 38, st36);
            StockManagement stmgt178 = new StockManagement(178, new DateTime(2023, 7, 10), StockMovementType.Saida, 43, st36);
            StockManagement stmgt179 = new StockManagement(179, new DateTime(2023, 7, 11), StockMovementType.Entrada, 24, st36);
            StockManagement stmgt180 = new StockManagement(180, new DateTime(2023, 7, 12), StockMovementType.Saida, 11, st36);
            StockManagement stmgt181 = new StockManagement(181, new DateTime(2023, 7, 13), StockMovementType.Saida, 46, st37);
            StockManagement stmgt182 = new StockManagement(182, new DateTime(2023, 7, 14), StockMovementType.Entrada, 38, st37);
            StockManagement stmgt183 = new StockManagement(183, new DateTime(2023, 7, 15), StockMovementType.Saida, 30, st37);
            StockManagement stmgt184 = new StockManagement(184, new DateTime(2023, 7, 16), StockMovementType.Entrada, 34, st37);
            StockManagement stmgt185 = new StockManagement(185, new DateTime(2023, 7, 17), StockMovementType.Saida, 44, st37);
            StockManagement stmgt186 = new StockManagement(186, new DateTime(2023, 7, 18), StockMovementType.Saida, 54, st38);
            StockManagement stmgt187 = new StockManagement(187, new DateTime(2023, 7, 19), StockMovementType.Entrada, 45, st38);
            StockManagement stmgt188 = new StockManagement(188, new DateTime(2023, 7, 20), StockMovementType.Saida, 47, st38);
            StockManagement stmgt189 = new StockManagement(189, new DateTime(2023, 7, 21), StockMovementType.Entrada, 30, st38);
            StockManagement stmgt190 = new StockManagement(190, new DateTime(2023, 7, 22), StockMovementType.Saida, 20, st38);
            StockManagement stmgt191 = new StockManagement(191, new DateTime(2023, 7, 23), StockMovementType.Saida, 10, st39);
            StockManagement stmgt192 = new StockManagement(192, new DateTime(2023, 7, 24), StockMovementType.Entrada, 55, st39);
            StockManagement stmgt193 = new StockManagement(193, new DateTime(2023, 7, 25), StockMovementType.Saida, 20, st39);
            StockManagement stmgt194 = new StockManagement(194, new DateTime(2023, 7, 26), StockMovementType.Entrada, 25, st39);
            StockManagement stmgt195 = new StockManagement(195, new DateTime(2023, 7, 27), StockMovementType.Saida, 59, st39);
            StockManagement stmgt196 = new StockManagement(196, new DateTime(2023, 7, 28), StockMovementType.Saida, 25, st40);
            StockManagement stmgt197 = new StockManagement(197, new DateTime(2023, 8, 1), StockMovementType.Entrada, 42, st40);
            StockManagement stmgt198 = new StockManagement(198, new DateTime(2023, 8, 2), StockMovementType.Saida, 33, st40);
            StockManagement stmgt199 = new StockManagement(199, new DateTime(2023, 8, 3), StockMovementType.Entrada, 52, st40);
            StockManagement stmgt200 = new StockManagement(200, new DateTime(2023, 8, 4), StockMovementType.Saida, 32, st40);
            StockManagement stmgt201 = new StockManagement(201, new DateTime(2023, 8, 5), StockMovementType.Saida, 20, st41);
            StockManagement stmgt202 = new StockManagement(202, new DateTime(2023, 8, 6), StockMovementType.Entrada, 34, st41);
            StockManagement stmgt203 = new StockManagement(203, new DateTime(2023, 8, 7), StockMovementType.Saida, 20, st41);
            StockManagement stmgt204 = new StockManagement(204, new DateTime(2023, 8, 8), StockMovementType.Entrada, 40, st41);
            StockManagement stmgt205 = new StockManagement(205, new DateTime(2023, 8, 9), StockMovementType.Saida, 26, st41);
            StockManagement stmgt206 = new StockManagement(206, new DateTime(2023, 8, 10), StockMovementType.Saida, 22, st42);
            StockManagement stmgt207 = new StockManagement(207, new DateTime(2023, 8, 11), StockMovementType.Entrada, 58, st42);
            StockManagement stmgt208 = new StockManagement(208, new DateTime(2023, 8, 12), StockMovementType.Saida, 18, st42);
            StockManagement stmgt209 = new StockManagement(209, new DateTime(2023, 8, 13), StockMovementType.Entrada, 27, st42);
            StockManagement stmgt210 = new StockManagement(210, new DateTime(2023, 8, 14), StockMovementType.Saida, 12, st42);
            StockManagement stmgt211 = new StockManagement(211, new DateTime(2023, 8, 15), StockMovementType.Saida, 31, st43);
            StockManagement stmgt212 = new StockManagement(212, new DateTime(2023, 8, 16), StockMovementType.Entrada, 27, st43);
            StockManagement stmgt213 = new StockManagement(213, new DateTime(2023, 8, 17), StockMovementType.Saida, 15, st43);
            StockManagement stmgt214 = new StockManagement(214, new DateTime(2023, 8, 18), StockMovementType.Entrada, 10, st43);
            StockManagement stmgt215 = new StockManagement(215, new DateTime(2023, 8, 19), StockMovementType.Saida, 32, st43);
            StockManagement stmgt216 = new StockManagement(216, new DateTime(2023, 8, 20), StockMovementType.Saida, 55, st44);
            StockManagement stmgt217 = new StockManagement(217, new DateTime(2023, 8, 21), StockMovementType.Entrada, 36, st44);
            StockManagement stmgt218 = new StockManagement(218, new DateTime(2023, 8, 22), StockMovementType.Saida, 16, st44);
            StockManagement stmgt219 = new StockManagement(219, new DateTime(2023, 8, 23), StockMovementType.Entrada, 14, st44);
            StockManagement stmgt220 = new StockManagement(220, new DateTime(2023, 8, 24), StockMovementType.Saida, 51, st44);
            StockManagement stmgt221 = new StockManagement(221, new DateTime(2023, 8, 25), StockMovementType.Saida, 58, st45);
            StockManagement stmgt222 = new StockManagement(222, new DateTime(2023, 8, 26), StockMovementType.Entrada, 14, st45);
            StockManagement stmgt223 = new StockManagement(223, new DateTime(2023, 8, 27), StockMovementType.Saida, 47, st45);
            StockManagement stmgt224 = new StockManagement(224, new DateTime(2023, 8, 28), StockMovementType.Entrada, 30, st45);
            StockManagement stmgt225 = new StockManagement(225, new DateTime(2023, 9, 1), StockMovementType.Saida, 10, st45);
            StockManagement stmgt226 = new StockManagement(226, new DateTime(2023, 9, 2), StockMovementType.Saida, 18, st46);
            StockManagement stmgt227 = new StockManagement(227, new DateTime(2023, 9, 3), StockMovementType.Entrada, 53, st46);
            StockManagement stmgt228 = new StockManagement(228, new DateTime(2023, 9, 4), StockMovementType.Saida, 55, st46);
            StockManagement stmgt229 = new StockManagement(229, new DateTime(2023, 9, 5), StockMovementType.Entrada, 28, st46);
            StockManagement stmgt230 = new StockManagement(230, new DateTime(2023, 9, 6), StockMovementType.Saida, 44, st46);
            StockManagement stmgt231 = new StockManagement(231, new DateTime(2023, 9, 7), StockMovementType.Saida, 45, st47);
            StockManagement stmgt232 = new StockManagement(232, new DateTime(2023, 9, 8), StockMovementType.Entrada, 15, st47);
            StockManagement stmgt233 = new StockManagement(233, new DateTime(2023, 9, 9), StockMovementType.Saida, 30, st47);
            StockManagement stmgt234 = new StockManagement(234, new DateTime(2023, 9, 10), StockMovementType.Entrada, 17, st47);
            StockManagement stmgt235 = new StockManagement(235, new DateTime(2023, 9, 11), StockMovementType.Saida, 28, st47);
            StockManagement stmgt236 = new StockManagement(236, new DateTime(2023, 9, 12), StockMovementType.Saida, 38, st48);
            StockManagement stmgt237 = new StockManagement(237, new DateTime(2023, 9, 13), StockMovementType.Entrada, 25, st48);
            StockManagement stmgt238 = new StockManagement(238, new DateTime(2023, 9, 14), StockMovementType.Saida, 35, st48);
            StockManagement stmgt239 = new StockManagement(239, new DateTime(2023, 9, 15), StockMovementType.Entrada, 36, st48);
            StockManagement stmgt240 = new StockManagement(240, new DateTime(2023, 9, 16), StockMovementType.Saida, 55, st48);
            StockManagement stmgt241 = new StockManagement(241, new DateTime(2023, 9, 17), StockMovementType.Saida, 31, st49);
            StockManagement stmgt242 = new StockManagement(242, new DateTime(2023, 9, 18), StockMovementType.Entrada, 49, st49);
            StockManagement stmgt243 = new StockManagement(243, new DateTime(2023, 9, 19), StockMovementType.Saida, 46, st49);
            StockManagement stmgt244 = new StockManagement(244, new DateTime(2023, 9, 20), StockMovementType.Entrada, 34, st49);
            StockManagement stmgt245 = new StockManagement(245, new DateTime(2023, 9, 21), StockMovementType.Saida, 57, st49);
            StockManagement stmgt246 = new StockManagement(246, new DateTime(2023, 9, 22), StockMovementType.Saida, 19, st50);
            StockManagement stmgt247 = new StockManagement(247, new DateTime(2023, 9, 23), StockMovementType.Entrada, 26, st50);
            StockManagement stmgt248 = new StockManagement(248, new DateTime(2023, 9, 24), StockMovementType.Saida, 19, st50);
            StockManagement stmgt249 = new StockManagement(249, new DateTime(2023, 9, 25), StockMovementType.Entrada, 54, st50);
            StockManagement stmgt250 = new StockManagement(250, new DateTime(2023, 9, 26), StockMovementType.Saida, 20, st50);
            StockManagement stmgt251 = new StockManagement(251, new DateTime(2023, 9, 27), StockMovementType.Saida, 47, st51);
            StockManagement stmgt252 = new StockManagement(252, new DateTime(2023, 9, 28), StockMovementType.Entrada, 23, st51);
            StockManagement stmgt253 = new StockManagement(253, new DateTime(2023, 10, 1), StockMovementType.Saida, 35, st51);
            StockManagement stmgt254 = new StockManagement(254, new DateTime(2023, 10, 2), StockMovementType.Entrada, 52, st51);
            StockManagement stmgt255 = new StockManagement(255, new DateTime(2023, 10, 3), StockMovementType.Saida, 51, st51);
            StockManagement stmgt256 = new StockManagement(256, new DateTime(2023, 10, 4), StockMovementType.Saida, 24, st52);
            StockManagement stmgt257 = new StockManagement(257, new DateTime(2023, 10, 5), StockMovementType.Entrada, 22, st52);
            StockManagement stmgt258 = new StockManagement(258, new DateTime(2023, 10, 6), StockMovementType.Saida, 37, st52);
            StockManagement stmgt259 = new StockManagement(259, new DateTime(2023, 10, 7), StockMovementType.Entrada, 48, st52);
            StockManagement stmgt260 = new StockManagement(260, new DateTime(2023, 10, 8), StockMovementType.Saida, 26, st52);
            StockManagement stmgt261 = new StockManagement(261, new DateTime(2023, 10, 9), StockMovementType.Saida, 47, st53);
            StockManagement stmgt262 = new StockManagement(262, new DateTime(2023, 10, 10), StockMovementType.Entrada, 58, st53);
            StockManagement stmgt263 = new StockManagement(263, new DateTime(2023, 10, 11), StockMovementType.Saida, 36, st53);
            StockManagement stmgt264 = new StockManagement(264, new DateTime(2023, 10, 12), StockMovementType.Entrada, 26, st53);
            StockManagement stmgt265 = new StockManagement(265, new DateTime(2023, 10, 13), StockMovementType.Saida, 45, st53);
            StockManagement stmgt266 = new StockManagement(266, new DateTime(2023, 10, 14), StockMovementType.Saida, 12, st54);
            StockManagement stmgt267 = new StockManagement(267, new DateTime(2023, 10, 15), StockMovementType.Entrada, 56, st54);
            StockManagement stmgt268 = new StockManagement(268, new DateTime(2023, 10, 16), StockMovementType.Saida, 45, st54);
            StockManagement stmgt269 = new StockManagement(269, new DateTime(2023, 10, 17), StockMovementType.Entrada, 11, st54);
            StockManagement stmgt270 = new StockManagement(270, new DateTime(2023, 10, 18), StockMovementType.Saida, 42, st54);
            StockManagement stmgt271 = new StockManagement(271, new DateTime(2023, 10, 19), StockMovementType.Saida, 44, st55);
            StockManagement stmgt272 = new StockManagement(272, new DateTime(2023, 10, 20), StockMovementType.Entrada, 28, st55);
            StockManagement stmgt273 = new StockManagement(273, new DateTime(2023, 10, 21), StockMovementType.Saida, 20, st55);
            StockManagement stmgt274 = new StockManagement(274, new DateTime(2023, 10, 22), StockMovementType.Entrada, 58, st55);
            StockManagement stmgt275 = new StockManagement(275, new DateTime(2023, 10, 23), StockMovementType.Saida, 14, st55);
            StockManagement stmgt276 = new StockManagement(276, new DateTime(2023, 10, 24), StockMovementType.Saida, 36, st56);
            StockManagement stmgt277 = new StockManagement(277, new DateTime(2023, 10, 25), StockMovementType.Entrada, 36, st56);
            StockManagement stmgt278 = new StockManagement(278, new DateTime(2023, 10, 26), StockMovementType.Saida, 50, st56);
            StockManagement stmgt279 = new StockManagement(279, new DateTime(2023, 10, 27), StockMovementType.Entrada, 55, st56);
            StockManagement stmgt280 = new StockManagement(280, new DateTime(2023, 10, 28), StockMovementType.Saida, 20, st56);
            StockManagement stmgt281 = new StockManagement(281, new DateTime(2023, 11, 1), StockMovementType.Saida, 52, st57);
            StockManagement stmgt282 = new StockManagement(282, new DateTime(2023, 11, 2), StockMovementType.Entrada, 42, st57);
            StockManagement stmgt283 = new StockManagement(283, new DateTime(2023, 11, 3), StockMovementType.Saida, 43, st57);
            StockManagement stmgt284 = new StockManagement(284, new DateTime(2023, 11, 4), StockMovementType.Entrada, 15, st57);
            StockManagement stmgt285 = new StockManagement(285, new DateTime(2023, 11, 5), StockMovementType.Saida, 11, st57);
            StockManagement stmgt286 = new StockManagement(286, new DateTime(2023, 11, 6), StockMovementType.Saida, 48, st58);
            StockManagement stmgt287 = new StockManagement(287, new DateTime(2023, 11, 7), StockMovementType.Entrada, 48, st58);
            StockManagement stmgt288 = new StockManagement(288, new DateTime(2023, 11, 8), StockMovementType.Saida, 50, st58);
            StockManagement stmgt289 = new StockManagement(289, new DateTime(2023, 11, 9), StockMovementType.Entrada, 40, st58);
            StockManagement stmgt290 = new StockManagement(290, new DateTime(2023, 11, 10), StockMovementType.Saida, 39, st58);
            StockManagement stmgt291 = new StockManagement(291, new DateTime(2023, 11, 11), StockMovementType.Saida, 48, st59);
            StockManagement stmgt292 = new StockManagement(292, new DateTime(2023, 11, 12), StockMovementType.Entrada, 45, st59);
            StockManagement stmgt293 = new StockManagement(293, new DateTime(2023, 11, 13), StockMovementType.Saida, 59, st59);
            StockManagement stmgt294 = new StockManagement(294, new DateTime(2023, 11, 14), StockMovementType.Entrada, 45, st59);
            StockManagement stmgt295 = new StockManagement(295, new DateTime(2023, 11, 15), StockMovementType.Saida, 34, st59);
            StockManagement stmgt296 = new StockManagement(296, new DateTime(2023, 11, 16), StockMovementType.Saida, 45, st60);
            StockManagement stmgt297 = new StockManagement(297, new DateTime(2023, 11, 17), StockMovementType.Entrada, 17, st60);
            StockManagement stmgt298 = new StockManagement(298, new DateTime(2023, 11, 18), StockMovementType.Saida, 58, st60);
            StockManagement stmgt299 = new StockManagement(299, new DateTime(2023, 11, 19), StockMovementType.Entrada, 39, st60);
            StockManagement stmgt300 = new StockManagement(300, new DateTime(2023, 11, 20), StockMovementType.Saida, 47, st60);
            StockManagement stmgt301 = new StockManagement(301, new DateTime(2023, 11, 21), StockMovementType.Saida, 15, st61);
            StockManagement stmgt302 = new StockManagement(302, new DateTime(2023, 11, 22), StockMovementType.Entrada, 25, st61);
            StockManagement stmgt303 = new StockManagement(303, new DateTime(2023, 11, 23), StockMovementType.Saida, 55, st61);
            StockManagement stmgt304 = new StockManagement(304, new DateTime(2023, 11, 24), StockMovementType.Entrada, 26, st61);
            StockManagement stmgt305 = new StockManagement(305, new DateTime(2023, 11, 25), StockMovementType.Saida, 59, st61);
            StockManagement stmgt306 = new StockManagement(306, new DateTime(2023, 11, 26), StockMovementType.Saida, 36, st62);
            StockManagement stmgt307 = new StockManagement(307, new DateTime(2023, 11, 27), StockMovementType.Entrada, 53, st62);
            StockManagement stmgt308 = new StockManagement(308, new DateTime(2023, 11, 28), StockMovementType.Saida, 52, st62);
            StockManagement stmgt309 = new StockManagement(309, new DateTime(2023, 12, 1), StockMovementType.Entrada, 20, st62);
            StockManagement stmgt310 = new StockManagement(310, new DateTime(2023, 12, 2), StockMovementType.Saida, 50, st62);
            StockManagement stmgt311 = new StockManagement(311, new DateTime(2023, 12, 3), StockMovementType.Saida, 52, st63);
            StockManagement stmgt312 = new StockManagement(312, new DateTime(2023, 12, 4), StockMovementType.Entrada, 47, st63);
            StockManagement stmgt313 = new StockManagement(313, new DateTime(2023, 12, 5), StockMovementType.Saida, 25, st63);
            StockManagement stmgt314 = new StockManagement(314, new DateTime(2023, 12, 6), StockMovementType.Entrada, 24, st63);
            StockManagement stmgt315 = new StockManagement(315, new DateTime(2023, 12, 7), StockMovementType.Saida, 43, st63);
            StockManagement stmgt316 = new StockManagement(316, new DateTime(2023, 12, 8), StockMovementType.Saida, 55, st64);
            StockManagement stmgt317 = new StockManagement(317, new DateTime(2023, 12, 9), StockMovementType.Entrada, 46, st64);
            StockManagement stmgt318 = new StockManagement(318, new DateTime(2023, 12, 10), StockMovementType.Saida, 43, st64);
            StockManagement stmgt319 = new StockManagement(319, new DateTime(2023, 12, 11), StockMovementType.Entrada, 42, st64);
            StockManagement stmgt320 = new StockManagement(320, new DateTime(2023, 12, 12), StockMovementType.Saida, 24, st64);
            StockManagement stmgt321 = new StockManagement(321, new DateTime(2023, 12, 13), StockMovementType.Saida, 17, st65);
            StockManagement stmgt322 = new StockManagement(322, new DateTime(2023, 12, 14), StockMovementType.Entrada, 23, st65);
            StockManagement stmgt323 = new StockManagement(323, new DateTime(2023, 12, 15), StockMovementType.Saida, 43, st65);
            StockManagement stmgt324 = new StockManagement(324, new DateTime(2023, 12, 16), StockMovementType.Entrada, 45, st65);
            StockManagement stmgt325 = new StockManagement(325, new DateTime(2023, 12, 17), StockMovementType.Saida, 50, st65);
            StockManagement stmgt326 = new StockManagement(326, new DateTime(2023, 12, 18), StockMovementType.Saida, 27, st66);
            StockManagement stmgt327 = new StockManagement(327, new DateTime(2023, 12, 19), StockMovementType.Entrada, 41, st66);
            StockManagement stmgt328 = new StockManagement(328, new DateTime(2023, 12, 20), StockMovementType.Saida, 42, st66);
            StockManagement stmgt329 = new StockManagement(329, new DateTime(2023, 12, 21), StockMovementType.Entrada, 16, st66);
            StockManagement stmgt330 = new StockManagement(330, new DateTime(2023, 12, 22), StockMovementType.Saida, 31, st66);
            StockManagement stmgt331 = new StockManagement(331, new DateTime(2023, 12, 23), StockMovementType.Saida, 34, st67);
            StockManagement stmgt332 = new StockManagement(332, new DateTime(2023, 12, 24), StockMovementType.Entrada, 31, st67);
            StockManagement stmgt333 = new StockManagement(333, new DateTime(2023, 12, 25), StockMovementType.Saida, 54, st67);
            StockManagement stmgt334 = new StockManagement(334, new DateTime(2023, 12, 26), StockMovementType.Entrada, 11, st67);
            StockManagement stmgt335 = new StockManagement(335, new DateTime(2023, 12, 27), StockMovementType.Saida, 10, st67);
            StockManagement stmgt336 = new StockManagement(336, new DateTime(2023, 12, 28), StockMovementType.Saida, 26, st68);
            StockManagement stmgt337 = new StockManagement(337, new DateTime(2024, 1, 1), StockMovementType.Entrada, 59, st68);
            StockManagement stmgt338 = new StockManagement(338, new DateTime(2024, 1, 2), StockMovementType.Saida, 38, st68);
            StockManagement stmgt339 = new StockManagement(339, new DateTime(2024, 1, 3), StockMovementType.Entrada, 58, st68);
            StockManagement stmgt340 = new StockManagement(340, new DateTime(2024, 1, 4), StockMovementType.Saida, 34, st68);
            StockManagement stmgt341 = new StockManagement(341, new DateTime(2024, 1, 5), StockMovementType.Saida, 53, st69);
            StockManagement stmgt342 = new StockManagement(342, new DateTime(2024, 1, 6), StockMovementType.Entrada, 36, st69);
            StockManagement stmgt343 = new StockManagement(343, new DateTime(2024, 1, 7), StockMovementType.Saida, 50, st69);
            StockManagement stmgt344 = new StockManagement(344, new DateTime(2024, 1, 8), StockMovementType.Entrada, 14, st69);
            StockManagement stmgt345 = new StockManagement(345, new DateTime(2024, 1, 9), StockMovementType.Saida, 50, st69);
            StockManagement stmgt346 = new StockManagement(346, new DateTime(2024, 1, 10), StockMovementType.Saida, 32, st70);
            StockManagement stmgt347 = new StockManagement(347, new DateTime(2024, 1, 11), StockMovementType.Entrada, 23, st70);
            StockManagement stmgt348 = new StockManagement(348, new DateTime(2024, 1, 12), StockMovementType.Saida, 17, st70);
            StockManagement stmgt349 = new StockManagement(349, new DateTime(2024, 1, 13), StockMovementType.Entrada, 34, st70);
            StockManagement stmgt350 = new StockManagement(350, new DateTime(2024, 1, 14), StockMovementType.Saida, 25, st70);
            StockManagement stmgt351 = new StockManagement(351, new DateTime(2024, 1, 15), StockMovementType.Saida, 27, st71);
            StockManagement stmgt352 = new StockManagement(352, new DateTime(2024, 1, 16), StockMovementType.Entrada, 28, st71);
            StockManagement stmgt353 = new StockManagement(353, new DateTime(2024, 1, 17), StockMovementType.Saida, 52, st71);
            StockManagement stmgt354 = new StockManagement(354, new DateTime(2024, 1, 18), StockMovementType.Entrada, 16, st71);
            StockManagement stmgt355 = new StockManagement(355, new DateTime(2024, 1, 19), StockMovementType.Saida, 26, st71);
            StockManagement stmgt356 = new StockManagement(356, new DateTime(2024, 1, 20), StockMovementType.Saida, 28, st72);
            StockManagement stmgt357 = new StockManagement(357, new DateTime(2024, 1, 21), StockMovementType.Entrada, 45, st72);
            StockManagement stmgt358 = new StockManagement(358, new DateTime(2024, 1, 22), StockMovementType.Saida, 52, st72);
            StockManagement stmgt359 = new StockManagement(359, new DateTime(2024, 1, 23), StockMovementType.Entrada, 12, st72);
            StockManagement stmgt360 = new StockManagement(360, new DateTime(2024, 1, 24), StockMovementType.Saida, 36, st72);
            StockManagement stmgt361 = new StockManagement(361, new DateTime(2024, 1, 25), StockMovementType.Saida, 45, st73);
            StockManagement stmgt362 = new StockManagement(362, new DateTime(2024, 1, 26), StockMovementType.Entrada, 34, st73);
            StockManagement stmgt363 = new StockManagement(363, new DateTime(2024, 1, 27), StockMovementType.Saida, 13, st73);
            StockManagement stmgt364 = new StockManagement(364, new DateTime(2024, 1, 28), StockMovementType.Entrada, 29, st73);
            StockManagement stmgt365 = new StockManagement(365, new DateTime(2024, 2, 1), StockMovementType.Saida, 26, st73);
            StockManagement stmgt366 = new StockManagement(366, new DateTime(2024, 2, 2), StockMovementType.Saida, 42, st74);
            StockManagement stmgt367 = new StockManagement(367, new DateTime(2024, 2, 3), StockMovementType.Entrada, 16, st74);
            StockManagement stmgt368 = new StockManagement(368, new DateTime(2024, 2, 4), StockMovementType.Saida, 16, st74);
            StockManagement stmgt369 = new StockManagement(369, new DateTime(2024, 2, 5), StockMovementType.Entrada, 15, st74);
            StockManagement stmgt370 = new StockManagement(370, new DateTime(2024, 2, 6), StockMovementType.Saida, 23, st74);
            StockManagement stmgt371 = new StockManagement(371, new DateTime(2024, 2, 7), StockMovementType.Saida, 38, st75);
            StockManagement stmgt372 = new StockManagement(372, new DateTime(2024, 2, 8), StockMovementType.Entrada, 19, st75);
            StockManagement stmgt373 = new StockManagement(373, new DateTime(2024, 2, 9), StockMovementType.Saida, 13, st75);
            StockManagement stmgt374 = new StockManagement(374, new DateTime(2024, 2, 10), StockMovementType.Entrada, 52, st75);
            StockManagement stmgt375 = new StockManagement(375, new DateTime(2024, 2, 11), StockMovementType.Saida, 43, st75);
            StockManagement stmgt376 = new StockManagement(376, new DateTime(2024, 2, 12), StockMovementType.Saida, 19, st76);
            StockManagement stmgt377 = new StockManagement(377, new DateTime(2024, 2, 13), StockMovementType.Entrada, 25, st76);
            StockManagement stmgt378 = new StockManagement(378, new DateTime(2024, 2, 14), StockMovementType.Saida, 14, st76);
            StockManagement stmgt379 = new StockManagement(379, new DateTime(2024, 2, 15), StockMovementType.Entrada, 50, st76);
            StockManagement stmgt380 = new StockManagement(380, new DateTime(2024, 2, 16), StockMovementType.Saida, 22, st76);
            StockManagement stmgt381 = new StockManagement(381, new DateTime(2024, 2, 17), StockMovementType.Saida, 56, st77);
            StockManagement stmgt382 = new StockManagement(382, new DateTime(2024, 2, 18), StockMovementType.Entrada, 11, st77);
            StockManagement stmgt383 = new StockManagement(383, new DateTime(2024, 2, 19), StockMovementType.Saida, 35, st77);
            StockManagement stmgt384 = new StockManagement(384, new DateTime(2024, 2, 20), StockMovementType.Entrada, 29, st77);
            StockManagement stmgt385 = new StockManagement(385, new DateTime(2024, 2, 21), StockMovementType.Saida, 17, st77);
            StockManagement stmgt386 = new StockManagement(386, new DateTime(2024, 2, 22), StockMovementType.Saida, 11, st78);
            StockManagement stmgt387 = new StockManagement(387, new DateTime(2024, 2, 23), StockMovementType.Entrada, 31, st78);
            StockManagement stmgt388 = new StockManagement(388, new DateTime(2024, 2, 24), StockMovementType.Saida, 34, st78);
            StockManagement stmgt389 = new StockManagement(389, new DateTime(2024, 2, 25), StockMovementType.Entrada, 30, st78);
            StockManagement stmgt390 = new StockManagement(390, new DateTime(2024, 2, 26), StockMovementType.Saida, 14, st78);
            StockManagement stmgt391 = new StockManagement(391, new DateTime(2024, 2, 27), StockMovementType.Saida, 57, st79);
            StockManagement stmgt392 = new StockManagement(392, new DateTime(2024, 2, 28), StockMovementType.Entrada, 49, st79);
            StockManagement stmgt393 = new StockManagement(393, new DateTime(2024, 3, 1), StockMovementType.Saida, 19, st79);
            StockManagement stmgt394 = new StockManagement(394, new DateTime(2024, 3, 2), StockMovementType.Entrada, 34, st79);
            StockManagement stmgt395 = new StockManagement(395, new DateTime(2024, 3, 3), StockMovementType.Saida, 43, st79);
            StockManagement stmgt396 = new StockManagement(396, new DateTime(2024, 3, 4), StockMovementType.Saida, 47, st80);
            StockManagement stmgt397 = new StockManagement(397, new DateTime(2024, 3, 5), StockMovementType.Entrada, 20, st80);
            StockManagement stmgt398 = new StockManagement(398, new DateTime(2024, 3, 6), StockMovementType.Saida, 47, st80);
            StockManagement stmgt399 = new StockManagement(399, new DateTime(2024, 3, 7), StockMovementType.Entrada, 18, st80);
            StockManagement stmgt400 = new StockManagement(400, new DateTime(2024, 3, 8), StockMovementType.Saida, 35, st80);
            StockManagement stmgt401 = new StockManagement(401, new DateTime(2024, 3, 9), StockMovementType.Saida, 57, st81);
            StockManagement stmgt402 = new StockManagement(402, new DateTime(2024, 3, 10), StockMovementType.Entrada, 14, st81);
            StockManagement stmgt403 = new StockManagement(403, new DateTime(2024, 3, 11), StockMovementType.Saida, 25, st81);
            StockManagement stmgt404 = new StockManagement(404, new DateTime(2024, 3, 12), StockMovementType.Entrada, 50, st81);
            StockManagement stmgt405 = new StockManagement(405, new DateTime(2024, 3, 13), StockMovementType.Saida, 18, st81);
            StockManagement stmgt406 = new StockManagement(406, new DateTime(2024, 3, 14), StockMovementType.Saida, 53, st82);
            StockManagement stmgt407 = new StockManagement(407, new DateTime(2024, 3, 15), StockMovementType.Entrada, 30, st82);
            StockManagement stmgt408 = new StockManagement(408, new DateTime(2024, 3, 16), StockMovementType.Saida, 59, st82);
            StockManagement stmgt409 = new StockManagement(409, new DateTime(2024, 3, 17), StockMovementType.Entrada, 19, st82);
            StockManagement stmgt410 = new StockManagement(410, new DateTime(2024, 3, 18), StockMovementType.Saida, 49, st82);
            StockManagement stmgt411 = new StockManagement(411, new DateTime(2024, 3, 19), StockMovementType.Saida, 53, st83);
            StockManagement stmgt412 = new StockManagement(412, new DateTime(2024, 3, 20), StockMovementType.Entrada, 29, st83);
            StockManagement stmgt413 = new StockManagement(413, new DateTime(2024, 3, 21), StockMovementType.Saida, 59, st83);
            StockManagement stmgt414 = new StockManagement(414, new DateTime(2024, 3, 22), StockMovementType.Entrada, 48, st83);
            StockManagement stmgt415 = new StockManagement(415, new DateTime(2024, 3, 23), StockMovementType.Saida, 11, st83);
            StockManagement stmgt416 = new StockManagement(416, new DateTime(2024, 3, 24), StockMovementType.Saida, 24, st84);
            StockManagement stmgt417 = new StockManagement(417, new DateTime(2024, 3, 25), StockMovementType.Entrada, 36, st84);
            StockManagement stmgt418 = new StockManagement(418, new DateTime(2024, 3, 26), StockMovementType.Saida, 38, st84);
            StockManagement stmgt419 = new StockManagement(419, new DateTime(2024, 3, 27), StockMovementType.Entrada, 16, st84);
            StockManagement stmgt420 = new StockManagement(420, new DateTime(2024, 3, 28), StockMovementType.Saida, 41, st84);
            StockManagement stmgt421 = new StockManagement(421, new DateTime(2024, 4, 1), StockMovementType.Saida, 42, st85);
            StockManagement stmgt422 = new StockManagement(422, new DateTime(2024, 4, 2), StockMovementType.Entrada, 25, st85);
            StockManagement stmgt423 = new StockManagement(423, new DateTime(2024, 4, 3), StockMovementType.Saida, 25, st85);
            StockManagement stmgt424 = new StockManagement(424, new DateTime(2024, 4, 4), StockMovementType.Entrada, 27, st85);
            StockManagement stmgt425 = new StockManagement(425, new DateTime(2024, 4, 5), StockMovementType.Saida, 39, st85);
            StockManagement stmgt426 = new StockManagement(426, new DateTime(2024, 4, 6), StockMovementType.Saida, 29, st86);
            StockManagement stmgt427 = new StockManagement(427, new DateTime(2024, 4, 7), StockMovementType.Entrada, 57, st86);
            StockManagement stmgt428 = new StockManagement(428, new DateTime(2024, 4, 8), StockMovementType.Saida, 52, st86);
            StockManagement stmgt429 = new StockManagement(429, new DateTime(2024, 4, 9), StockMovementType.Entrada, 47, st86);
            StockManagement stmgt430 = new StockManagement(430, new DateTime(2024, 4, 10), StockMovementType.Saida, 15, st86);
            StockManagement stmgt431 = new StockManagement(431, new DateTime(2024, 4, 11), StockMovementType.Saida, 52, st87);
            StockManagement stmgt432 = new StockManagement(432, new DateTime(2024, 4, 12), StockMovementType.Entrada, 29, st87);
            StockManagement stmgt433 = new StockManagement(433, new DateTime(2024, 4, 13), StockMovementType.Saida, 26, st87);
            StockManagement stmgt434 = new StockManagement(434, new DateTime(2024, 4, 14), StockMovementType.Entrada, 52, st87);
            StockManagement stmgt435 = new StockManagement(435, new DateTime(2024, 4, 15), StockMovementType.Saida, 55, st87);
            StockManagement stmgt436 = new StockManagement(436, new DateTime(2024, 4, 16), StockMovementType.Saida, 25, st88);
            StockManagement stmgt437 = new StockManagement(437, new DateTime(2024, 4, 17), StockMovementType.Entrada, 53, st88);
            StockManagement stmgt438 = new StockManagement(438, new DateTime(2024, 4, 18), StockMovementType.Saida, 45, st88);
            StockManagement stmgt439 = new StockManagement(439, new DateTime(2024, 4, 19), StockMovementType.Entrada, 34, st88);
            StockManagement stmgt440 = new StockManagement(440, new DateTime(2024, 4, 20), StockMovementType.Saida, 49, st88);
            StockManagement stmgt441 = new StockManagement(441, new DateTime(2024, 4, 21), StockMovementType.Saida, 49, st89);
            StockManagement stmgt442 = new StockManagement(442, new DateTime(2024, 4, 22), StockMovementType.Entrada, 33, st89);
            StockManagement stmgt443 = new StockManagement(443, new DateTime(2024, 4, 23), StockMovementType.Saida, 55, st89);
            StockManagement stmgt444 = new StockManagement(444, new DateTime(2024, 4, 24), StockMovementType.Entrada, 12, st89);
            StockManagement stmgt445 = new StockManagement(445, new DateTime(2024, 4, 25), StockMovementType.Saida, 19, st89);
            StockManagement stmgt446 = new StockManagement(446, new DateTime(2024, 4, 26), StockMovementType.Saida, 19, st90);
            StockManagement stmgt447 = new StockManagement(447, new DateTime(2024, 4, 27), StockMovementType.Entrada, 52, st90);
            StockManagement stmgt448 = new StockManagement(448, new DateTime(2024, 4, 28), StockMovementType.Saida, 30, st90);
            StockManagement stmgt449 = new StockManagement(449, new DateTime(2024, 5, 1), StockMovementType.Entrada, 23, st90);
            StockManagement stmgt450 = new StockManagement(450, new DateTime(2024, 5, 2), StockMovementType.Saida, 48, st90);
            StockManagement stmgt451 = new StockManagement(451, new DateTime(2024, 5, 3), StockMovementType.Saida, 21, st91);
            StockManagement stmgt452 = new StockManagement(452, new DateTime(2024, 5, 4), StockMovementType.Entrada, 13, st91);
            StockManagement stmgt453 = new StockManagement(453, new DateTime(2024, 5, 5), StockMovementType.Saida, 11, st91);
            StockManagement stmgt454 = new StockManagement(454, new DateTime(2024, 5, 6), StockMovementType.Entrada, 32, st91);
            StockManagement stmgt455 = new StockManagement(455, new DateTime(2024, 5, 7), StockMovementType.Saida, 19, st91);
            StockManagement stmgt456 = new StockManagement(456, new DateTime(2024, 5, 8), StockMovementType.Saida, 38, st92);
            StockManagement stmgt457 = new StockManagement(457, new DateTime(2024, 5, 9), StockMovementType.Entrada, 35, st92);
            StockManagement stmgt458 = new StockManagement(458, new DateTime(2024, 5, 10), StockMovementType.Saida, 25, st92);
            StockManagement stmgt459 = new StockManagement(459, new DateTime(2024, 5, 11), StockMovementType.Entrada, 21, st92);
            StockManagement stmgt460 = new StockManagement(460, new DateTime(2024, 5, 12), StockMovementType.Saida, 24, st92);
            StockManagement stmgt461 = new StockManagement(461, new DateTime(2024, 5, 13), StockMovementType.Saida, 26, st93);
            StockManagement stmgt462 = new StockManagement(462, new DateTime(2024, 5, 14), StockMovementType.Entrada, 25, st93);
            StockManagement stmgt463 = new StockManagement(463, new DateTime(2024, 5, 15), StockMovementType.Saida, 33, st93);
            StockManagement stmgt464 = new StockManagement(464, new DateTime(2024, 5, 16), StockMovementType.Entrada, 51, st93);
            StockManagement stmgt465 = new StockManagement(465, new DateTime(2024, 5, 17), StockMovementType.Saida, 19, st93);
            StockManagement stmgt466 = new StockManagement(466, new DateTime(2024, 5, 18), StockMovementType.Saida, 41, st94);
            StockManagement stmgt467 = new StockManagement(467, new DateTime(2024, 5, 19), StockMovementType.Entrada, 59, st94);
            StockManagement stmgt468 = new StockManagement(468, new DateTime(2024, 5, 20), StockMovementType.Saida, 14, st94);
            StockManagement stmgt469 = new StockManagement(469, new DateTime(2024, 5, 21), StockMovementType.Entrada, 19, st94);
            StockManagement stmgt470 = new StockManagement(470, new DateTime(2024, 5, 22), StockMovementType.Saida, 49, st94);
            StockManagement stmgt471 = new StockManagement(471, new DateTime(2024, 5, 23), StockMovementType.Saida, 20, st95);
            StockManagement stmgt472 = new StockManagement(472, new DateTime(2024, 5, 24), StockMovementType.Entrada, 39, st95);
            StockManagement stmgt473 = new StockManagement(473, new DateTime(2024, 5, 25), StockMovementType.Saida, 14, st95);
            StockManagement stmgt474 = new StockManagement(474, new DateTime(2024, 5, 26), StockMovementType.Entrada, 35, st95);
            StockManagement stmgt475 = new StockManagement(475, new DateTime(2024, 5, 27), StockMovementType.Saida, 10, st95);
            StockManagement stmgt476 = new StockManagement(476, new DateTime(2024, 5, 28), StockMovementType.Saida, 13, st96);
            StockManagement stmgt477 = new StockManagement(477, new DateTime(2024, 6, 1), StockMovementType.Entrada, 21, st96);
            StockManagement stmgt478 = new StockManagement(478, new DateTime(2024, 6, 2), StockMovementType.Saida, 33, st96);
            StockManagement stmgt479 = new StockManagement(479, new DateTime(2024, 6, 3), StockMovementType.Entrada, 51, st96);
            StockManagement stmgt480 = new StockManagement(480, new DateTime(2024, 6, 4), StockMovementType.Saida, 21, st96);
            StockManagement stmgt481 = new StockManagement(481, new DateTime(2024, 6, 5), StockMovementType.Saida, 48, st97);
            StockManagement stmgt482 = new StockManagement(482, new DateTime(2024, 6, 6), StockMovementType.Entrada, 20, st97);
            StockManagement stmgt483 = new StockManagement(483, new DateTime(2024, 6, 7), StockMovementType.Saida, 30, st97);
            StockManagement stmgt484 = new StockManagement(484, new DateTime(2024, 6, 8), StockMovementType.Entrada, 51, st97);
            StockManagement stmgt485 = new StockManagement(485, new DateTime(2024, 6, 9), StockMovementType.Saida, 38, st97);
            StockManagement stmgt486 = new StockManagement(486, new DateTime(2024, 6, 10), StockMovementType.Saida, 27, st98);
            StockManagement stmgt487 = new StockManagement(487, new DateTime(2024, 6, 11), StockMovementType.Entrada, 28, st98);
            StockManagement stmgt488 = new StockManagement(488, new DateTime(2024, 6, 12), StockMovementType.Saida, 23, st98);
            StockManagement stmgt489 = new StockManagement(489, new DateTime(2024, 6, 13), StockMovementType.Entrada, 25, st98);
            StockManagement stmgt490 = new StockManagement(490, new DateTime(2024, 6, 14), StockMovementType.Saida, 25, st98);
            StockManagement stmgt491 = new StockManagement(491, new DateTime(2024, 6, 15), StockMovementType.Saida, 45, st99);
            StockManagement stmgt492 = new StockManagement(492, new DateTime(2024, 6, 16), StockMovementType.Entrada, 39, st99);
            StockManagement stmgt493 = new StockManagement(493, new DateTime(2024, 6, 17), StockMovementType.Saida, 42, st99);
            StockManagement stmgt494 = new StockManagement(494, new DateTime(2024, 6, 18), StockMovementType.Entrada, 49, st99);
            StockManagement stmgt495 = new StockManagement(495, new DateTime(2024, 6, 19), StockMovementType.Saida, 51, st99);
            StockManagement stmgt496 = new StockManagement(496, new DateTime(2024, 6, 20), StockMovementType.Saida, 40, st100);
            StockManagement stmgt497 = new StockManagement(497, new DateTime(2024, 6, 21), StockMovementType.Entrada, 22, st100);
            StockManagement stmgt498 = new StockManagement(498, new DateTime(2024, 6, 22), StockMovementType.Saida, 41, st100);
            StockManagement stmgt499 = new StockManagement(499, new DateTime(2024, 6, 23), StockMovementType.Entrada, 11, st100);
            StockManagement stmgt500 = new StockManagement(500, new DateTime(2024, 6, 24), StockMovementType.Saida, 16, st100);



            
            _context.AddRange(st1, st2, st3, st4, st5, st6, st7, st8, st9, st10, st11,
                                st12, st13, st14, st15, st16, st17, st18, st19, st20, st21, st22, st23, st24, st25, st26, st27,
                                st28, st29, st30, st31, st32, st33, st34, st35, st36, st37, st38, st39, st40, st41, st42, st43,
                                st44, st45, st46, st47, st48, st49, st50, st51, st52, st53, st54, st55, st56, st57, st58, st59,
                                st60, st61, st62, st63, st64, st65, st66, st67, st68, st69, st70, st71, st72, st73, st74, st75,
                                st76, st77, st78, st79, st80, st81, st82, st83, st84, st85, st86, st87, st88, st89, st90, st91,
                                st92, st93, st94, st95, st96, st97, st98, st99, st100);

            _context.AddRange(
                                stmgt1, stmgt2, stmgt3, stmgt4, stmgt5, stmgt6, stmgt7, stmgt8, stmgt9, stmgt10, stmgt11,
                                stmgt12, stmgt13, stmgt14, stmgt15, stmgt16, stmgt17, stmgt18, stmgt19, stmgt20, stmgt21, stmgt22, stmgt23, stmgt24, stmgt25, stmgt26, stmgt27,
                                stmgt28, stmgt29, stmgt30, stmgt31, stmgt32, stmgt33, stmgt34, stmgt35, stmgt36, stmgt37, stmgt38, stmgt39, stmgt40, stmgt41, stmgt42, stmgt43,
                                stmgt44, stmgt45, stmgt46, stmgt47, stmgt48, stmgt49, stmgt50, stmgt51, stmgt52, stmgt53, stmgt54, stmgt55, stmgt56, stmgt57, stmgt58, stmgt59,
                                stmgt60, stmgt61, stmgt62, stmgt63, stmgt64, stmgt65, stmgt66, stmgt67, stmgt68, stmgt69, stmgt70, stmgt71, stmgt72, stmgt73, stmgt74, stmgt75,
                                stmgt76, stmgt77, stmgt78, stmgt79, stmgt80, stmgt81, stmgt82, stmgt83, stmgt84, stmgt85, stmgt86, stmgt87, stmgt88, stmgt89, stmgt90, stmgt91,
                                stmgt92, stmgt93, stmgt94, stmgt95, stmgt96, stmgt97, stmgt98, stmgt99, stmgt100, stmgt101, stmgt102, stmgt103, stmgt104, stmgt105, stmgt106,
                                stmgt107, stmgt108, stmgt109, stmgt110, stmgt111, stmgt112, stmgt113, stmgt114, stmgt115, stmgt116, stmgt117, stmgt118, stmgt119, stmgt120,
                                stmgt121, stmgt122, stmgt123, stmgt124, stmgt125, stmgt126, stmgt127, stmgt128, stmgt129, stmgt130, stmgt131, stmgt132, stmgt133, stmgt134,
                                stmgt135, stmgt136, stmgt137, stmgt138, stmgt139, stmgt140, stmgt141, stmgt142, stmgt143, stmgt144, stmgt145, stmgt146, stmgt147, stmgt148, stmgt149,
                                stmgt150, stmgt151, stmgt152, stmgt153, stmgt154, stmgt155, stmgt156, stmgt157, stmgt158, stmgt159, stmgt160, stmgt161, stmgt162, stmgt163, stmgt164,
                                stmgt165, stmgt166, stmgt167, stmgt168, stmgt169, stmgt170, stmgt171, stmgt172, stmgt173, stmgt174, stmgt175, stmgt176, stmgt177, stmgt178, stmgt179,
                                stmgt180, stmgt181, stmgt182, stmgt183, stmgt184, stmgt185, stmgt186, stmgt187, stmgt188, stmgt189, stmgt190, stmgt191, stmgt192, stmgt193, stmgt194,
                                stmgt195, stmgt196, stmgt197, stmgt198, stmgt199, stmgt200, stmgt201, stmgt202, stmgt203, stmgt204, stmgt205, stmgt206, stmgt207, stmgt208, stmgt209,
                                stmgt210, stmgt211, stmgt212, stmgt213, stmgt214, stmgt215, stmgt216, stmgt217, stmgt218, stmgt219, stmgt220, stmgt221, stmgt222, stmgt223, stmgt224,
                                stmgt225, stmgt226, stmgt227, stmgt228, stmgt229, stmgt230, stmgt231, stmgt232, stmgt233, stmgt234, stmgt235, stmgt236, stmgt237, stmgt238, stmgt239,
                                stmgt240, stmgt241, stmgt242, stmgt243, stmgt244, stmgt245, stmgt246, stmgt247, stmgt248, stmgt249, stmgt250, stmgt251, stmgt252, stmgt253, stmgt254,
                                stmgt255, stmgt256, stmgt257, stmgt258, stmgt259, stmgt260, stmgt261, stmgt262, stmgt263, stmgt264, stmgt265, stmgt266, stmgt267, stmgt268, stmgt269,
                                stmgt270, stmgt271, stmgt272, stmgt273, stmgt274, stmgt275, stmgt276, stmgt277, stmgt278, stmgt279, stmgt280, stmgt281, stmgt282, stmgt283, stmgt284,
                                stmgt285, stmgt286, stmgt287, stmgt288, stmgt289, stmgt290, stmgt291, stmgt292, stmgt293, stmgt294, stmgt295, stmgt296, stmgt297, stmgt298, stmgt299,
                                stmgt300, stmgt301, stmgt302, stmgt303, stmgt304, stmgt305, stmgt306, stmgt307, stmgt308, stmgt309, stmgt310, stmgt311, stmgt312, stmgt313, stmgt314,
                                stmgt315, stmgt316, stmgt317, stmgt318, stmgt319, stmgt320, stmgt321, stmgt322, stmgt323, stmgt324, stmgt325, stmgt326, stmgt327, stmgt328, stmgt329,
                                stmgt330, stmgt331, stmgt332, stmgt333, stmgt334, stmgt335, stmgt336, stmgt337, stmgt338, stmgt339, stmgt340, stmgt341, stmgt342, stmgt343, stmgt344,
                                stmgt345, stmgt346, stmgt347, stmgt348, stmgt349, stmgt350, stmgt351, stmgt352, stmgt353, stmgt354, stmgt355, stmgt356, stmgt357, stmgt358, stmgt359,
                                stmgt360, stmgt361, stmgt362, stmgt363, stmgt364, stmgt365, stmgt366, stmgt367, stmgt368, stmgt369, stmgt370, stmgt371, stmgt372, stmgt373, stmgt374,
                                stmgt375, stmgt376, stmgt377, stmgt378, stmgt379, stmgt380, stmgt381, stmgt382, stmgt383, stmgt384, stmgt385, stmgt386, stmgt387, stmgt388, stmgt389,
                                stmgt390, stmgt391, stmgt392, stmgt393, stmgt394, stmgt395, stmgt396, stmgt397, stmgt398, stmgt399, stmgt400, stmgt401, stmgt402, stmgt403, stmgt404,
                                stmgt405, stmgt406, stmgt407, stmgt408, stmgt409, stmgt410, stmgt411, stmgt412, stmgt413, stmgt414, stmgt415, stmgt416, stmgt417, stmgt418, stmgt419,
                                stmgt420, stmgt421, stmgt422, stmgt423, stmgt424, stmgt425, stmgt426, stmgt427, stmgt428, stmgt429, stmgt430, stmgt431, stmgt432, stmgt433, stmgt434,
                                stmgt435, stmgt436, stmgt437, stmgt438, stmgt439, stmgt440, stmgt441, stmgt442, stmgt443, stmgt444, stmgt445, stmgt446, stmgt447, stmgt448, stmgt449,
                                stmgt450, stmgt451, stmgt452, stmgt453, stmgt454, stmgt455, stmgt456, stmgt457, stmgt458, stmgt459, stmgt460, stmgt461, stmgt462, stmgt463, stmgt464,
                                stmgt465, stmgt466, stmgt467, stmgt468, stmgt469, stmgt470, stmgt471, stmgt472, stmgt473, stmgt474, stmgt475, stmgt476, stmgt477, stmgt478, stmgt479,
                                stmgt480, stmgt481, stmgt482, stmgt483, stmgt484, stmgt485, stmgt486, stmgt487, stmgt488, stmgt489, stmgt490, stmgt491, stmgt492, stmgt493, stmgt494,
                                stmgt495, stmgt496, stmgt497, stmgt498, stmgt499, stmgt500
);
            _context.SaveChanges();

        }


    }
}
*/