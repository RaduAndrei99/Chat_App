/***************************************************************************
 *                                                                         *
 *  Autor:  Cojocaru Constantin-Cosmin                                     *
 *  Grupa:  1309A                                                          *
 *  Fisier: IDatabaseConnection.cs                                         *
 *                                                                         *
 *  Descriere: Interfata pentru o conexiune la o baza de date generica     *
 *                                                                         *
 ***************************************************************************/

using Oracle.ManagedDataAccess.Client;

namespace Model.DatabaseConnection
{
    public interface IDatabaseConnection
    {
        uint Connect();
        void CloseConnection(uint connection);
        OracleConnection Connection(uint connection);
    }
}
