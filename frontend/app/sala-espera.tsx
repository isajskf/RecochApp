import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  TouchableOpacity,
  FlatList,
} from 'react-native';

import { useLocalSearchParams, router } from 'expo-router';

export default function SalaEspera() {
  const { modo, jugadores, tiempo } = useLocalSearchParams();

  const listaJugadores = [
    { id: '1', nombre: 'Jugador 1' },
    { id: '2', nombre: 'Jugador 2' },
    { id: '3', nombre: 'Jugador 3' },
  ];

  const codigoSala = 'ABC123';

  return (
    <SafeAreaView style={styles.container}>

      <Text style={styles.title}>Sala de Espera 🎮</Text>

      {/* INFO REAL */}
      <View style={styles.infoBox}>
        <Text style={styles.infoText}>Modo: {modo}</Text>
        <Text style={styles.infoText}>Jugadores: {jugadores}</Text>
        <Text style={styles.infoText}>Tiempo: {tiempo}</Text>
      </View>

      {/* CODIGO */}
      <View style={styles.codeBox}>
        <Text style={styles.codeLabel}>Código de la sala</Text>
        <Text style={styles.code}>{codigoSala}</Text>
      </View>

      {/* JUGADORES */}
      <View style={styles.playersContainer}>
        <Text style={styles.sectionTitle}>Jugadores conectados</Text>

        <FlatList
          data={listaJugadores}
          keyExtractor={(item) => item.id}
          renderItem={({ item }) => (
            <View style={styles.playerCard}>
              <Text style={styles.playerText}>{item.nombre}</Text>
            </View>
          )}
        />
      </View>

      {/* BOTON */}
      <TouchableOpacity
        style={styles.button}
        onPress={() => router.push('/juego')}
      >
        <Text style={styles.buttonText}>Iniciar Partida 🚀</Text>
      </TouchableOpacity>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121212',
    padding: 20,
  },

  title: {
    fontSize: 32,
    color: '#fff',
    fontWeight: 'bold',
    textAlign: 'center',
    marginTop: 20,
    marginBottom: 20,
  },

  infoBox: {
    backgroundColor: '#1f1f1f',
    padding: 15,
    borderRadius: 12,
    marginBottom: 15,
  },

  infoText: {
    color: '#fff',
    fontSize: 14,
    marginBottom: 5,
  },

  codeBox: {
    backgroundColor: '#1f1f1f',
    padding: 20,
    borderRadius: 16,
    alignItems: 'center',
    marginBottom: 20,
  },

  codeLabel: {
    color: '#aaa',
  },

  code: {
    color: '#7c3aed',
    fontSize: 28,
    fontWeight: 'bold',
    letterSpacing: 3,
  },

  playersContainer: {
    flex: 1,
  },

  sectionTitle: {
    color: '#fff',
    marginBottom: 10,
    fontSize: 16,
  },

  playerCard: {
    backgroundColor: '#1f1f1f',
    padding: 15,
    borderRadius: 12,
    marginBottom: 10,
  },

  playerText: {
    color: '#fff',
  },

  button: {
    backgroundColor: '#7c3aed',
    padding: 18,
    borderRadius: 16,
    alignItems: 'center',
    marginTop: 10,
  },

  buttonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
});