using System.IO;
using System.Text;

ReadFile();
Console.ReadKey();


static void ReadFile()
{
    string file = @"C:\Users\digis\Documents\RobertoVelazquezGonzalez\BlocDeNotas\LayoutUsuarios.txt";

    if (File.Exists(file))
    {

        StreamReader Textfile = new StreamReader(file);

        string line;
        line = Textfile.ReadLine();

        while ((line = Textfile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            ML.Usuario usuario = new ML.Usuario();

            usuario.UserName = lines[0];
            usuario.Nombre = lines[1];
            usuario.ApellidoPaterno = lines[2];
            usuario.ApellidoMaterno = lines[3];
            usuario.Email = lines[4];
            usuario.Password = lines[5];
            usuario.FechaNacimiento = lines[6];
            usuario.Sexo = lines[7];
            usuario.Telefono = lines[8];
            usuario.Celular = lines[9];
            usuario.CURP = lines[10];

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = byte.Parse(lines[11]);

            usuario.Imagen = null;

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[12];
            usuario.Direccion.NumeroInterior = lines[13];
            usuario.Direccion.NumeroExterior = lines[14];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = byte.Parse(lines[15]);

            ML.Result result = BL.Usuario.Add(usuario);


            if (result.Correct)
            {
                Console.WriteLine("Correcto");
                Console.ReadKey();
            }
            else
            {
                string filePath = @"C:\Users\digis\Documents\RobertoVelazquezGonzalez\BlocDeNotas\ErroresLayout.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine(result.ErrorMessage);
                }
            }
        }
    }
}