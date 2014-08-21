using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;

namespace FLEX.Common.IO
{
   public static class BufferedIO
   {
      private const int TagLength = 7;

      private static readonly BufferPool Pool = new BufferPool();
      private static readonly Random Rand = new Random();

      
   }

   internal sealed class BufferPool
   {
      private readonly ConcurrentStack<byte[]> _pool = new ConcurrentStack<byte[]>();
      private readonly int _bufferSize = Configuration.Instance.BufferSizeInBytesForBufferedIO;
      private readonly int _maxPoolSize = Configuration.Instance.BufferPoolCountForBufferedIO;

      /// <summary>
      ///   Ritorna un buffer dal pool, se disponibile, o ne alloca uno nuovo.
      /// </summary>
      /// <returns></returns>
      public byte[] AcquireBuffer()
      {
         // Se il pool contiene un elemento libero, lo ritorno,
         // altrimenti devo allocare un nuovo vettore di byte.
         byte[] buffer;
         return _pool.TryPop(out buffer) ? buffer : new byte[_bufferSize];
      }

      public void ReleaseBuffer(byte[] buffer)
      {
         // La seguente operazione non è del tutto concorrente, ma renderla tale
         // prevederebbe l'uso di un lock. Ci accontentiamo del fatto che, nonostante
         // la leggera imperfezione, la dimensione del pool sia vagamente uguale al massimo.
         if (_pool.Count < _maxPoolSize)
         {
            _pool.Push(buffer);
         }
         // Alla fine delle varie operazioni, la coda _non_ dovrebbe superare
         // la lunghezza massima indicata a tempo di costruzione.
         Debug.Assert(_pool.Count <= _maxPoolSize);
      }
   }

   /// <summary>
   ///   Rappresenta un percorso temporaneo che deve essere eliminato dopo l'uso.
   /// </summary>
   /// <remarks>
   ///   Occorre invocare il metodo <see cref="TemporaryPath.Delete"/> dopo aver usato il path.
   /// </remarks>
   [Serializable]
   public struct TemporaryPath
   {
      /// <summary>
      ///   Il percorso temporaneo che, prima o poi, dovrà essere cancellato.
      /// </summary>
      public string Path { get; set; }

      /// <summary>
      ///   Cancella il percorso puntato, se esiste ancora.
      /// </summary>
      public void Delete()
      {
         if (Path != null && File.Exists(Path))
         {
            File.Delete(Path);
            Path = null;
         }
      }
   }
}
