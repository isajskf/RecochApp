import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  SafeAreaView,
  Image,
  Dimensions,
  ScrollView,
} from 'react-native';

import { useState, useEffect } from 'react';

import { router } from 'expo-router';

import { LinearGradient } from 'expo-linear-gradient';

import * as ScreenOrientation from 'expo-screen-orientation';

const { width, height } = Dimensions.get('window');

export default function CrearSala() {

  useEffect(() => {
    ScreenOrientation.lockAsync(
      ScreenOrientation.OrientationLock.LANDSCAPE
    );
  }, []);

  const [modoSeleccionado, setModoSeleccionado] = useState('');

  const modos = [
    {
      id: 'TRIVIA',
      emoji: '🤔',
      color: '#ff2ec4',
      descripcion: 'Reta tus conocimientos',
      jugadores: '3-10 jugadores',
      intensidad: 'Media',
    },

    {
      id: 'RETOS',
      emoji: '🤣',
      color: '#1565c0',
      descripcion: 'Diversión pura',
      jugadores: '2-10 jugadores',
      intensidad: 'Media',
    },

    {
      id: 'PAREJA',
      emoji: '💕',
      color: '#8d2a84',
      descripcion: 'Romántico y atrevido',
      jugadores: '2 jugadores',
      intensidad: 'Alta',
    },

    {
      id: 'FIESTA',
      emoji: '🎉',
      color: '#8e44ad',
      descripcion: 'Perfecto para grupos',
      jugadores: '2-10 jugadores',
      intensidad: 'Media',
    },
  ];

  return (
    <LinearGradient
      colors={['#1d022e', '#25053a', '#2d0b45']}
      style={styles.container}
    >

      <SafeAreaView style={styles.safe}>

        <ScrollView
          contentContainerStyle={styles.scroll}
          showsVerticalScrollIndicator={false}
        >

          {/* CARD PRINCIPAL */}
          <View style={styles.card}>

            {/* HEADER */}
            <View style={styles.headerRow}>

              <Image
                source={require('./assets/images/logo.png')}
                style={styles.logo}
              />

            </View>

            {/* SUBTITULO */}
            <Text style={styles.subTitle}>
              - Modos de juego -
            </Text>

            {/* MODOS */}
            <View style={styles.modosContainer}>

              {modos.map((modo) => (

                <TouchableOpacity
                  key={modo.id}
                  activeOpacity={0.8}
                  onPress={() =>
                    setModoSeleccionado(modo.id)
                  }
                  style={[
                    styles.modoCard,
                    {
                      backgroundColor: modo.color,
                    },

                    modoSeleccionado === modo.id &&
                      styles.selectedCard,
                  ]}
                >

                  <Text style={styles.emoji}>
                    {modo.emoji}
                  </Text>

                  <Text style={styles.cardTitle}>
                    Modo{'\n'}{modo.id}
                  </Text>

                  <Text style={styles.cardDescription}>
                    {modo.descripcion}
                  </Text>

                  <Text style={styles.cardInfo}>
                    {modo.jugadores}
                  </Text>

                  <Text style={styles.cardInfo}>
                    Intensidad: {modo.intensidad}
                  </Text>

                </TouchableOpacity>

              ))}

            </View>

            {/* PERSONALIZAR */}
            <Text style={styles.personalizar}>
              ⚙️ Personaliza tu partida
            </Text>

            {/* CONFIG */}
            <View style={styles.bottomRow}>

              {/* JUGADORES */}
              <View style={styles.playersContainer}>

                <Text style={styles.bottomLabel}>
                  Jugadores:
                </Text>

                <View style={styles.counter}>

                  <TouchableOpacity>
                    <Text style={styles.counterButton}>
                      -
                    </Text>
                  </TouchableOpacity>

                  <Text style={styles.counterText}>
                    5
                  </Text>

                  <TouchableOpacity>
                    <Text style={styles.counterButton}>
                      +
                    </Text>
                  </TouchableOpacity>

                </View>

              </View>

              {/* TIEMPO */}
              <View style={styles.timeContainer}>

                <Text style={styles.bottomLabel}>
                  Tiempo:
                </Text>

                <TouchableOpacity style={styles.timeButton}>
                  <Text style={styles.timeText}>
                    10 mins ▼
                  </Text>
                </TouchableOpacity>

              </View>

              {/* CREAR SALA */}
              <TouchableOpacity
                style={styles.startButton}
                onPress={() => router.push('/sala-espera')}
              >
                <Text style={styles.startText}>
                  Crear sala
                </Text>
              </TouchableOpacity>

            </View>

            {/* VOLVER */}
            <TouchableOpacity
              style={styles.backButton}
              onPress={() => router.back()}
            >
              <Text style={styles.backText}>
                ← Volver al menú
              </Text>
            </TouchableOpacity>

          </View>

        </ScrollView>

      </SafeAreaView>

    </LinearGradient>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
  },

  safe: {
    flex: 1,
  },

  scroll: {
    flexGrow: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingVertical: 20,
  },

  card: {
    width: '92%',
    minHeight: '90%',
    backgroundColor: '#220335',
    borderRadius: 35,
    paddingVertical: 20,
    paddingHorizontal: 22,
    justifyContent: 'space-between',
  },

  headerRow: {
    width: '100%',
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 5,
    marginBottom: 10,
  },

  logo: {
    width: width * 0.30,
    height: 65,
    resizeMode: 'contain',
  },

  subTitle: {
    color: '#fff',
    textAlign: 'center',
    marginBottom: 8,
    marginTop: -2,
    fontSize: width * 0.014,
  },

  modosContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 10,
  },

  modoCard: {
    width: '22.5%',
    borderRadius: 22,
    padding: 12,
    height: height * 0.46,
    justifyContent: 'flex-start',
  },

  selectedCard: {
    borderWidth: 4,
    borderColor: '#fff',
  },

  emoji: {
    fontSize: 28,
    marginBottom: 5,
  },

  cardTitle: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: width * 0.024,
    marginBottom: 4,
  },

  cardDescription: {
    color: '#fff',
    fontSize: width * 0.012,
    marginBottom: 8,
  },

  cardInfo: {
    color: '#fff',
    fontSize: width * 0.011,
    marginTop: 4,
  },

  personalizar: {
    color: '#fff',
    textAlign: 'center',
    fontSize: width * 0.02,
    marginTop: 40,
    marginBottom: 18,
    fontWeight: 'bold',
  },

  bottomRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },

  playersContainer: {
    width: '28%',
  },

  timeContainer: {
    width: '22%',
  },

  bottomLabel: {
    color: '#fff',
    marginBottom: 8,
    fontWeight: 'bold',
    fontSize: width * 0.013,
  },

  counter: {
    flexDirection: 'row',
    backgroundColor: '#ff4fd8',
    borderRadius: 30,
    alignItems: 'center',
    justifyContent: 'space-around',
    paddingVertical: 8,
  },

  counterButton: {
    color: '#fff',
    fontSize: 24,
    fontWeight: 'bold',
  },

  counterText: {
    color: '#fff',
    fontSize: width * 0.015,
    fontWeight: 'bold',
  },

  timeButton: {
    backgroundColor: '#ff4fd8',
    paddingVertical: 10,
    borderRadius: 20,
    alignItems: 'center',
  },

  timeText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: width * 0.012,
  },

  startButton: {
    backgroundColor: '#ff00b7',
    width: '25%',
    paddingVertical: 13,
    borderRadius: 20,
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 18,
  },

  startText: {
    color: '#fff',
    fontSize: width * 0.013,
    fontWeight: 'bold',
  },

  backButton: {
    alignSelf: 'center',
    marginTop: 10,
    backgroundColor: '#c13bd3',
    paddingHorizontal: 24,
    paddingVertical: 10,
    borderRadius: 20,
  },

  backText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: width * 0.013,
  },

});