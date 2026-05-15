import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  TextInput,
  TouchableOpacity,
} from 'react-native';

import { useState } from 'react';

export default function Juego() {
  const [respuesta, setRespuesta] = useState('');
  const [resultado, setResultado] = useState('');
  const [puntaje, setPuntaje] = useState(0);
  const [turno, setTurno] = useState<'host' | 'player'>('player');

  // 🎮 SIMULACIÓN MULTIJUGADOR REAL
  const jugadorHost = 'Anfitrión 👑';
  const jugador2 = 'Jugador ❤️';

  const pregunta = {
    categoria: 'Parejas ❤️',
    texto: '¿Qué tanto me conoces?',
    pista: 'Comida favorita 🍕',
    respuestaCorrecta: 'pizza',
  };

  const enviarRespuesta = () => {
    if (!respuesta) {
      setResultado('⚠️ Escribe una respuesta primero');
      return;
    }

    if (turno !== 'player') {
      setResultado('⏳ Esperando turno del jugador...');
      return;
    }

    const correcta =
      respuesta.toLowerCase().trim() ===
      pregunta.respuestaCorrecta.toLowerCase();

    if (correcta) {
      setResultado('✅ Correcto 😏');
      setPuntaje(puntaje + 1);
    } else {
      setResultado(`❌ Incorrecto (era: ${pregunta.respuestaCorrecta})`);
    }

    setTurno('host');
    setRespuesta('');
  };

  const siguienteTurno = () => {
    setTurno('player');
    setResultado('');
  };

  return (
    <SafeAreaView style={styles.container}>

      <Text style={styles.title}>Modo Parejas 🎮</Text>

      <View style={styles.players}>
        <Text style={styles.player}>{jugadorHost} vs {jugador2}</Text>
      </View>

      <Text style={styles.score}>⭐ Puntaje: {puntaje}</Text>

      <View style={styles.card}>
        <Text style={styles.category}>{pregunta.categoria}</Text>
        <Text style={styles.question}>{pregunta.texto}</Text>
        <Text style={styles.hint}>💡 {pregunta.pista}</Text>
      </View>

      <Text style={styles.turno}>
        🎯 Turno: {turno === 'player' ? jugador2 : jugadorHost}
      </Text>

      <TextInput
        style={styles.input}
        placeholder="Escribe tu respuesta..."
        placeholderTextColor="#777"
        value={respuesta}
        onChangeText={setRespuesta}
        editable={turno === 'player'}
      />

      <TouchableOpacity style={styles.button} onPress={enviarRespuesta}>
        <Text style={styles.buttonText}>Enviar Respuesta</Text>
      </TouchableOpacity>

      <TouchableOpacity
        style={[styles.button, { backgroundColor: '#444', marginTop: 10 }]}
        onPress={siguienteTurno}
      >
        <Text style={styles.buttonText}>Siguiente Turno 🔄</Text>
      </TouchableOpacity>

      {resultado !== '' && (
        <View style={styles.resultBox}>
          <Text style={styles.resultText}>{resultado}</Text>
        </View>
      )}

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121212',
    padding: 20,
    justifyContent: 'center',
  },

  title: {
    fontSize: 30,
    color: '#fff',
    fontWeight: 'bold',
    textAlign: 'center',
  },

  players: {
    marginTop: 10,
    marginBottom: 10,
  },

  player: {
    color: '#7c3aed',
    textAlign: 'center',
    fontWeight: 'bold',
  },

  score: {
    color: '#fff',
    textAlign: 'center',
    marginVertical: 10,
  },

  turno: {
    color: '#aaa',
    textAlign: 'center',
    marginBottom: 10,
  },

  card: {
    backgroundColor: '#1f1f1f',
    padding: 20,
    borderRadius: 16,
    marginVertical: 15,
    borderWidth: 1,
    borderColor: '#2d2d2d',
  },

  category: {
    color: '#7c3aed',
    textAlign: 'center',
    marginBottom: 10,
    fontWeight: 'bold',
  },

  question: {
    color: '#fff',
    fontSize: 18,
    textAlign: 'center',
    marginBottom: 10,
  },

  hint: {
    color: '#aaa',
    textAlign: 'center',
  },

  input: {
    backgroundColor: '#1f1f1f',
    color: '#fff',
    padding: 18,
    borderRadius: 14,
    borderWidth: 1,
    borderColor: '#2d2d2d',
    marginBottom: 10,
  },

  button: {
    backgroundColor: '#7c3aed',
    padding: 16,
    borderRadius: 14,
    alignItems: 'center',
  },

  buttonText: {
    color: '#fff',
    fontWeight: 'bold',
  },

  resultBox: {
    marginTop: 20,
    padding: 15,
    backgroundColor: '#1f1f1f',
    borderRadius: 12,
    borderWidth: 1,
    borderColor: '#2d2d2d',
  },

  resultText: {
    color: '#fff',
    textAlign: 'center',
  },
});