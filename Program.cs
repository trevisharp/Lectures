#region Car Controller Chalenge

/// Objetivo:
///         Controlar o carro para dar uma volta na pista
///
/// Esta atividade busca desenvolver capacidades de trabalhar com:
///         Lei de Murphy, Tópicos em Engenharia, Sensores e atoadores,
///         programação, injeção de dependência, lógica, criatividade,
///         inglês técnico, fisíca, matemática, ruído.
///
/// Instruções:
///         No método init você pode implementar instalações para isso,
///         basta adicionar o parâmetro do tipo Installer que permitira
///         a instalação de sensores no seu carro. Após isso implemente
///         o método run. Cada dispoitivo instalado pode ser recuperado
///         atráveis de um parâmetro na função run com o mesmo nome do
///         instalado no init. Exemplo:
///         
///         void init(Installer installer)
///         {
///             installer.InstallAccelerometer("acc");
///             installer.InstallLeftInfraredSensor("left");
///         }
///         
///         void run(Accelerometer acc, InfraredSensor left)
///         {
///             float x = acc.X; //Obtém a aceleração do carrinho em x 
///                              //a cada loop
///             float dist = left.Distance; //Obtém distância do carrinho
///                                         //a parede mais próxima a esquerda
///         }
///         
///         Para controlar o carrinho utilize o Controller. Para mostrar
///         variaveis na tela afim de debug utilize o Logger. Construa
///         sua solução a partir destes componentes.
///         
///         void run(Controller controller, Accelerometer acc, Logger log)
///         {
///             controller.EngineVoltage = 5f; //Define a tensão aplicada no motor elétrico
///             log.Clear(); //Limpa console
///             log.Print(acc.Y); //Mostra aceleração em Y
///         }
///         
///         Note que campos como Controller, Logger, Installer, 
///         Accelerometer serão preenchidos automatica conforme
///         declarados por injeção de dependência.
///         
///         Abaixo código básico de using e inicialização.
using CarControl;
GameManager.Run();

#endregion

void init(Installer installer)
{
    installer.InstallAccelerometer("acc");
    installer.InstallLeftInfraredSensor("left");
    installer.InstallRightInfraredSensors("right");
}

void run(Controller controller, Logger log, 
    Accelerometer acc, InfraredSensor left, InfraredSensor right)
{
    log.Print("Olá mundo");
}