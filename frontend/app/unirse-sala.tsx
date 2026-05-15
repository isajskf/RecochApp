import {
  View,
  Text,
  StyleSheet,
  TextInput,
  TouchableOpacity,
  SafeAreaView,
} from 'react-native';

import { useState } from 'react';
import { router } from 'expo-router';

export default function UnirseSala() {
  const [codigo, setCodigo] = useState('');

  return (
    <SafeAreaView style={styles.container}>

      <View style={styles.content}>
        
        <Text style={styles.title}>Unirse a Sala 🎉</Text>

        <Text style={styles.subtitle}>
          Ingresa el código de la sala para entrar a la partida
        </Text>

        <Text style={styles.label}>Código de sala</Text>

        <TextInput
          placeholder="Ej: ABC123"
          placeholderTextColor="#777"
          style={styles.input}
          value={codigo}
          onChangeText={setCodigo}
          autoCapitalize="characters"
        />

        <TouchableOpacity
          style={styles.button}
          onPress={() => router.push('/sala-espera')}>
          <Text style={styles.buttonText}>Entrar a la Sala</Text>
        </TouchableOpacity>

      </View>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121212',
    justifyContent: 'center',
    padding: 24,
  },

  content: {
    width: '100%',
  },

  title: {
    fontSize: 38,
    fontWeight: 'bold',
    color: '#fff',
    marginBottom: 10,
    textAlign: 'center',
  },

  subtitle: {
    color: '#aaa',
    fontSize: 16,
    textAlign: 'center',
    marginBottom: 40,
    lineHeight: 24,
  },

  label: {
    color: '#fff',
    marginBottom: 10,
    fontSize: 16,
    fontWeight: '600',
  },

  input: {
    backgroundColor: '#1f1f1f',
    color: '#fff',
    padding: 18,
    borderRadius: 16,
    marginBottom: 30,
    fontSize: 20,
    textAlign: 'center',
    letterSpacing: 3,
    borderWidth: 1,
    borderColor: '#2d2d2d',
  },

  button: {
    backgroundColor: '#7c3aed',
    paddingVertical: 18,
    borderRadius: 18,
    alignItems: 'center',
  },

  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});